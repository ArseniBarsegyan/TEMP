using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ManagerService.Models;
using Task04.DAL.EF;
using Task04.DAL.Entities;
using Task04.DAL.Repositories;

namespace ManagerService.Classes
{
    public class Logger
    {
        private readonly FileSystemWatcher _watcher;
        object _obj = new object();
        private object _lockobj = new object();
        private bool _enabled = true;
        private GenericRepository<Manager> _managerRepo;
        private GenericRepository<Product> _productRepo;

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

        private void WatcherOnChanged(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            RecordEntry("changed ", fileSystemEventArgs.FullPath);

            var task = new Task(() =>
            {
                WriteDataToDataBaseFromFile(fileSystemEventArgs.FullPath);
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
