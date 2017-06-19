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
    public class PurchaseService : IPurchaseService
    {
        private IdentityUnitOfWork UnitOfWork { get; }

        public PurchaseService(IdentityUnitOfWork unitOfWork)
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

            var manager = Mapper.Map<PurchaseDto, Manager>(purchaseDto);
            var client = Mapper.Map<PurchaseDto, Client>(purchaseDto);

            var product = UnitOfWork.ProductRepository.GetAll()
                .FirstOrDefault(x => x.Name == purchaseDto.ProductName);
            if (product == null)
            {
                product = Mapper.Map<PurchaseDto, Product>(purchaseDto);
                product.Manager = manager;
                product.Client = client;
                client.Product = product;
                UnitOfWork.ProductRepository.Create(product);
                UnitOfWork.ProductRepository.Save();
            }
            else
            {
                product.Manager = manager;
                product.Client = client;
                client.Product = product;
                UnitOfWork.ProductRepository.Update(product);
                UnitOfWork.ProductRepository.Save();
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