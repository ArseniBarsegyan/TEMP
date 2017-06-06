using System;
using System.IO;
using System.Linq;
using AutoMapper;
using TestMapper.Context;
using TestMapper.DAO;
using TestMapper.DTO;
using TestMapper.Repository;

namespace TestMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var streamReader = new StreamReader(@"D:\test_06062017.txt"))
            {
                var context = new AppContext("DefaultConnection");
                var managerRepo = new GenericRepository<Manager>(context);
                var productRepo = new GenericRepository<Product>(context);

                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(@"D:\test_06062017.txt");
                var splittedFileName = fileNameWithoutExtension.Split('_');
                var managerName = splittedFileName[0];
                var managerDate = splittedFileName[1];

                var purchaseDto = new PurchaseDto()
                {
                    ManagerName = managerName,
                    Date = DateTime.ParseExact(managerDate, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture)
                };

                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var lineContent = line.Split(',');
                    purchaseDto.ClientName = lineContent[0].Trim();
                    purchaseDto.ProductName = lineContent[1].Trim();
                    purchaseDto.ProductPrice = decimal.Parse(lineContent[2].Trim());

                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PurchaseDto, Product>()
                        .ForMember(x => x.Name, y => y.MapFrom(z => z.ProductName))
                        .ForMember(x => x.Price, y => y.MapFrom(z => z.ProductPrice))
                        .ForMember(x => x.Date, y => y.MapFrom(z => z.Date))
                        .ForMember(x => x.Client, y => y.MapFrom(z => z.ClientName));
                        cfg.CreateMap<PurchaseDto, Manager>()
                        .ForMember(x => x.Name, y => y.MapFrom(z => z.ManagerName));
                    });

                    var product = Mapper.Map<PurchaseDto, Product>(purchaseDto);

                    var manager = managerRepo.GetAll().FirstOrDefault(x => x.Name == purchaseDto.ManagerName);
                    if (manager == null)
                    {
                        manager = Mapper.Map<PurchaseDto, Manager>(purchaseDto);
                        product.Manager = manager;
                        manager.Products.Add(product);
                        managerRepo.Create(manager);
                        productRepo.Create(product);
                    }
                    else
                    {
                        product.Manager = manager;
                        manager.Products.Add(product);
                        managerRepo.Update(manager);
                        productRepo.Create(product);
                    }

                    managerRepo.Save();
                    productRepo.Save();
                }
            }

            //using (var context = new AppContext("DefaultConnection"))
            //{
            //    var purchaseDto = new PurchaseDto()
            //    {
            //        Date = DateTime.Now,
            //        ManagerName = "Manager-2",
            //        ProductName = "Car-3",
            //        ProductPrice = 300
            //    };

            //    Mapper.Initialize(cfg =>
            //    {
            //        cfg.CreateMap<PurchaseDto, Product>()
            //        .ForMember(x => x.Name, y => y.MapFrom(z => z.ProductName))
            //        .ForMember(x => x.Price, y => y.MapFrom(z => z.ProductPrice))
            //        .ForMember(x => x.Date, y => y.MapFrom(z => z.Date));
            //        cfg.CreateMap<PurchaseDto, Manager>()
            //        .ForMember(x => x.Name, y => y.MapFrom(z => z.ManagerName));
            //    });

            //    var product = Mapper.Map<PurchaseDto, Product>(purchaseDto);
            //    var manager = context.Managers.FirstOrDefault(x => x.Name == purchaseDto.ManagerName);

            //    if (manager == null)
            //    {
            //        manager = Mapper.Map<PurchaseDto, Manager>(purchaseDto);
            //        context.Managers.Add(manager);
            //    }

            //    product.Manager = manager;
            //    manager.Products.Add(product);
            //    context.Products.Add(product);
            //    context.SaveChanges();
            //}
        }
    }
}
