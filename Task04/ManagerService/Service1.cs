using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using ManagerService.Models;

namespace ManagerService
{
    public partial class Service1 : ServiceBase
    {
        private Logger _logger;
        
        public Service1()
        {
            InitializeComponent();
            CanStop = true;
            CanPauseAndContinue = true;
            AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            _logger = new Logger();
            var loggerTask = new Task(_logger.Start);
            loggerTask.Start();
        }

        protected override void OnStop()
        {
            _logger.Stop();
            Thread.Sleep(1000);
        }
    }

    class Logger
    {
        private readonly FileSystemWatcher _watcher;
        object _obj = new object();
        private bool _enabled = true;

        public Logger()
        {
            _watcher = new FileSystemWatcher(ConfigurationManager.AppSettings["ServerPath"]);

            _watcher.Created += WatcherOnCreated;
            _watcher.Changed += WatcherOnChanged;
        }

        public void Start()
        {
            _watcher.EnableRaisingEvents = true;
            while (_enabled)
            {
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            _watcher.EnableRaisingEvents = false;
            _enabled = false;
        }

        private void WatcherOnCreated(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            RecordEntry("created ", fileSystemEventArgs.FullPath);
        }

        //When file has changed start writing a file with all manager's fields
        private void WatcherOnChanged(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            RecordEntry("changed ", fileSystemEventArgs.FullPath);

            var task = new Task(() =>
            {
                var managerDto = CreateManagerDto(fileSystemEventArgs.FullPath);
                RecordManager(managerDto);
            });
            task.Start();
            task.Wait();
        }

        private void RecordEntry(string fileEvent, string filePath)
        {
            lock (_obj)
            {
                using (var writer = new StreamWriter(ConfigurationManager.AppSettings["LogPath"], true))
                {
                    writer.WriteLine($"{filePath} was {fileEvent}");
                    writer.Flush();
                }
            }
        }

        private ManagerDto CreateManagerDto(string fileName)
        {
            if (!File.Exists(fileName)) return null;
            var managerName = fileName.Substring(fileName.LastIndexOf('\\') + 1,
                fileName.Length - fileName.LastIndexOf('_') - 4);
            var managerDate = fileName.Substring(fileName.LastIndexOf('_') + 1, 
                fileName.Length + 4 - fileName.LastIndexOf('.'));
            var productsList = new List<ProductDto>();

            var manager = new ManagerDto()
            {
                LastName = managerName,
                Date = DateTime.ParseExact(managerDate, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture),
            };
            

            using (var reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var lineContent = line.Split(',');

                    productsList.Add(new ProductDto()
                    {
                        Client = lineContent[0].Trim(),
                        Name = lineContent[1].Trim(),
                        Price = decimal.Parse(lineContent[2].Trim()),
                        Manager = manager
                    });
                }
            }

            manager.Products = productsList;
            return manager;
        }

        private void RecordManager(ManagerDto manager)
        {
            using (var writer = new StreamWriter(ConfigurationManager.AppSettings["ManagerInfoStorage"] + 
                $"{manager.LastName}.txt"))
            {
                foreach (var product in manager.Products)
                {
                    writer.WriteLine($"{product.Name} {product.Client} {product.Price} {product.Manager.LastName}");
                }
            }
        }
    }
}
