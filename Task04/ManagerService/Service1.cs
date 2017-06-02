using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using ManagerService.Models;
using Task04.DAL.Context;

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
            _watcher = new FileSystemWatcher();
            _watcher.Created += WatcherOnCreated;
        }

        private void WatcherOnCreated(object sender, FileSystemEventArgs e)
        {
            var filePath = e.FullPath;
            RecordEntry(filePath);
        }

        //Generate FileContent model from existing *.scv file
        private FileContent GenerateFileContent(string fileName)
        {
            if (!File.Exists(fileName)) return null;
            using (var parser = new CsvParser(new StreamReader(fileName)))
            {
                var fileContent = new FileContent(fileName, new List<string>(), new Dictionary<string, decimal>());
                while (true)
                {
                    var row = parser.Read();
                    if (row == null)
                    {
                        break;
                    }

                    fileContent.AddClient(row[0]);
                    fileContent.AddProduct(row[1], decimal.Parse(row[2]));
                }
                return fileContent;
            }
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

        private void RecordEntry(string filePath)
        {
            lock (_obj)
            {
                using (var writer = new StreamWriter(ConfigurationManager.AppSettings["ServerPath"] + "log.txt", true))
                {
                    writer.WriteLine($"{filePath} was created");
                    writer.Flush();
                }
            }
        }
    }
}
