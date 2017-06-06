using System;
using AutoMapper;
using TestMapper.Context;
using TestMapper.DAO;
using TestMapper.DTO;

namespace TestMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new AppContext("DefaultConnection"))
            {
                var managerDto = new ManagerDto()
                {
                    Date = DateTime.Now,
                    ManagerName = "Manager-2",
                    ProductName = "Car-2",
                    ProductPrice = 240
                };

                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<ManagerDto, Product>()
                    .ForMember(x => x.Name, y => y.MapFrom(z => z.ProductName))
                    .ForMember(x => x.Price, y => y.MapFrom(z => z.ProductPrice))
                    .ForMember(x => x.Date, y => y.MapFrom(z => z.Date));
                    cfg.CreateMap<ManagerDto, Manager>()
                    .ForMember(x => x.Name, y => y.MapFrom(z => z.ManagerName));
                });

                var manager = Mapper.Map<ManagerDto, Manager>(managerDto);
                var product = Mapper.Map<ManagerDto, Product>(managerDto);
                manager.Products.Add(product);
                product.Manager = manager;

                context.Managers.Add(manager);
                context.Products.Add(product);
                context.SaveChanges();
            }
        }
    }
}
