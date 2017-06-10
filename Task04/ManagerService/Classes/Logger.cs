using System.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
<<<<<<< HEAD
=======
using AutoMapper;
using ManagerService.Models;
using Task04.DAL.EF;
using Task04.DAL.Entities;
using Task04.DAL.Repositories;
>>>>>>> origin/master

namespace ManagerService.Classes
{
    public class Logger
    {
        private readonly FileSystemWatcher _watcher;
<<<<<<< HEAD
        private object _obj = new object();
        private bool _enabled = true;
        private CsvDbRecorder _dbRecorder;
=======
        private readonly object _obj = new object();
        private object _lockobj = new object();
        private bool _enabled = true;
        private GenericRepository<Manager> _managerRepo;
>>>>>>> origin/master

        public Logger()
        {
            _watcher = new FileSystemWatcher(ConfigurationManager.AppSettings["ServerPath"]);
            _dbRecorder = new CsvDbRecorder("DefaultConnection");

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

        private void WatcherOnChanged(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            RecordEntry("changed ", fileSystemEventArgs.FullPath);

            try
            {
<<<<<<< HEAD
                _dbRecorder.WriteDataToDataBaseFromFile(fileSystemEventArgs.FullPath);
            });
            task.Start();
            task.Wait();
=======
                var task = new Task(() =>
                {
                    WriteDataToDataBaseFromFile(fileSystemEventArgs.FullPath);
                    //Delete file after recording to database, write to log about success
                    File.Delete(fileSystemEventArgs.FullPath);
                    RecordEntry("File has been successfully proceeded", fileSystemEventArgs.FullPath);
                });
                task.Start();
                task.Wait();
            }
            catch (ThreadInterruptedException)
            {
                //If something goes wrong move file from source path to directory
                //with failed files, write to log about success

                var sourceFullName = fileSystemEventArgs.FullPath;
                var destinationPath = ConfigurationManager.AppSettings["FailedFilesStorage"];
                var destinationFullName = destinationPath + Path.GetFileName(sourceFullName);

                RecordEntry("error occured when proceeded ", fileSystemEventArgs.FullPath);
                if (!Directory.Exists(destinationPath))
                {
                    Directory.CreateDirectory(ConfigurationManager.AppSettings["FailedFilesStorage"]);
                }

                if (File.Exists(destinationFullName))
                {
                    File.Delete(destinationFullName);
                }
                File.Move(sourceFullName, destinationFullName);
            }
>>>>>>> origin/master
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
<<<<<<< HEAD
=======

        private void WriteDataToDataBaseFromFile(string fileName)
        {
            _managerRepo = new GenericRepository<Manager>(new AppDbContext("DefaultConnection"));

            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            if (fileNameWithoutExtension == null) return;
            var splittedFileName = fileNameWithoutExtension.Split('_');
            var managerName = splittedFileName[0];
            var managerDate = splittedFileName[1];

            var purchaseDto = new PurchaseDto()
            {
                ManagerName = managerName,
                Date = DateTime.ParseExact(managerDate, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture),
            };

            using (var reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var lineContent = line.Split(',');
                    purchaseDto.ClientName = lineContent[0].Trim();
                    purchaseDto.ProductName = lineContent[1].Trim();
                    purchaseDto.Price = decimal.Parse(lineContent[2].Trim());

                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PurchaseDto, Product>()
                            .ForMember(x => x.Name, opt => opt.MapFrom(src => src.ProductName))
                            .ForMember(x => x.Date, opt => opt.MapFrom(src => src.Date))
                            .ForMember(x => x.Client, opt => opt.MapFrom(src => src.ClientName))
                            .ForMember(x => x.Price, opt => opt.MapFrom(src => src.Price));
                        cfg.CreateMap<PurchaseDto, Manager>()
                            .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.ManagerName));
                    });

                    var product = Mapper.Map<PurchaseDto, Product>(purchaseDto);

                    lock (_lockobj)
                    {
                        var manager = _managerRepo.GetAll().FirstOrDefault(x => x.LastName == purchaseDto.ManagerName);
                        if (manager == null)
                        {
                            manager = Mapper.Map<PurchaseDto, Manager>(purchaseDto);
                            manager.Products.Add(product);
                            _managerRepo.Create(manager);
                        }
                        else
                        {
                            manager.Products.Add(product);
                            _managerRepo.Update(manager);
                        }

                        _managerRepo.Save();
                    }
                    
                }
            }
        }
    }
}
