using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using UserStore.BLL.DTO;
using UserStore.BLL.Infrastructure;
using UserStore.BLL.Interfaces;
using UserStore.DAL.Repositories;
using UserStore.DAL.Entities;

namespace UserStore.BLL.Services
{
    public class SalesService : ISalesService
    {
        private IdentityUnitOfWork UnitOfWork { get; }

        public SalesService(IdentityUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IEnumerable<PurchaseDto> GetAllSalesList()
        {
            var salesList = new List<PurchaseDto>();
            var allProducts = UnitOfWork.ProductRepository.GetAll();
            
            foreach (var product in allProducts)
            {
                var saleDto = new PurchaseDto()
                {
                    ProductName = product.Name,
                    ClientName = product.Client.Name,
                    Date = product.Date,
                    ManagerName = product.Manager.LastName,
                    Price = product.Price
                };
                salesList.Add(saleDto);
            }
            return salesList;
        }

        public OperationDetails Create(PurchaseDto purchaseDto)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<PurchaseDto, Product>()
                    .ForMember(x => x.Name, opt => opt.MapFrom(src => src.ProductName))
                    .ForMember(x => x.Date, opt => opt.MapFrom(src => src.Date))
                    .ForMember(x => x.Price, opt => opt.MapFrom(src => src.Price));
                cfg.CreateMap<PurchaseDto, Manager>()
                    .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.ManagerName));
                cfg.CreateMap<PurchaseDto, Client>()
                    .ForMember(x => x.Name, opt => opt.MapFrom(src => src.ClientName));
            });

            var product = Mapper.Map<PurchaseDto, Product>(purchaseDto);
            var client = Mapper.Map<PurchaseDto, Client>(purchaseDto);
            client.Product = product;

            var manager = UnitOfWork.ManagerRepository.GetAll()
                .FirstOrDefault(x => x.LastName == purchaseDto.ManagerName);
            if (manager == null)
            {
                manager = Mapper.Map<PurchaseDto, Manager>(purchaseDto);
                manager.Products.Add(product);
                UnitOfWork.ManagerRepository.Create(manager);
                UnitOfWork.ManagerRepository.Save();
            }
            else
            {
                manager.Products.Add(product);
                UnitOfWork.ManagerRepository.Update(manager);
                UnitOfWork.ManagerRepository.Save();
            }

            return new OperationDetails(true, "successfull created", "");
        }

        public OperationDetails Edit(PurchaseDto saleDto)
        {
            throw new System.NotImplementedException();
        }

        public OperationDetails Delete(PurchaseDto saleDto)
        {
            throw new System.NotImplementedException();
        }
    }
}