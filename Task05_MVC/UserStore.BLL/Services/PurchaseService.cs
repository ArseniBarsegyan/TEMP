using System.Collections.Generic;
using System.Data.Entity;
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

        public PurchaseDto GetPurchaseDtoById(int id)
        {
            return GetAllSalesList().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<PurchaseDto> GetAllSalesList()
        {
            var salesList = new List<PurchaseDto>();
            var allProducts = UnitOfWork.ProductRepository.GetAll().Include(x => x.Client)
                .Include(x => x.Manager);
            
            foreach (var product in allProducts)
            {
                var saleDto = new PurchaseDto()
                {
                    Id = product.Id,
                    ClientName = product.Client.Name,
                    ManagerName = product.Manager.LastName,
                    ProductName = product.Name,
                    Date = product.Date,
                    Price = product.Price
                };
                salesList.Add(saleDto);
            }
            return salesList;
        }

        public OperationDetails Create(PurchaseDto purchaseDto)
        {
            InitializeMapper();
            var product = Mapper.Map<PurchaseDto, Product>(purchaseDto);

            var client = UnitOfWork.ClientRepository.GetAll()
                .FirstOrDefault(x => x.Name == purchaseDto.ClientName);

            if (client == null)
            {
                client = Mapper.Map<PurchaseDto, Client>(purchaseDto);
                UnitOfWork.ClientRepository.Create(client);
            }
            else
            {
                UnitOfWork.ClientRepository.Update(client);
            }
            UnitOfWork.ClientRepository.Save();
            product.Client = client;

            var manager = UnitOfWork.ManagerRepository.GetAll()
                .FirstOrDefault(x => x.LastName == purchaseDto.ManagerName);

            if (manager == null)
            {
                manager = Mapper.Map<PurchaseDto, Manager>(purchaseDto);
                manager.Products.Add(product);
                UnitOfWork.ManagerRepository.Create(manager);
            }
            else
            {
                manager.Products.Add(product);
                UnitOfWork.ManagerRepository.Update(manager);
            }
            UnitOfWork.ManagerRepository.Save();

            return new OperationDetails(true, "successfull created", "");
        }

        public OperationDetails Edit(PurchaseDto purchaseDto)
        {
            InitializeMapper();
            var product = UnitOfWork.ProductRepository.GetAll()
                .FirstOrDefault(x => x.Id == purchaseDto.Id);

            product.Name = purchaseDto.ProductName;
            product.Date = purchaseDto.Date;
            product.Price = purchaseDto.Price;
            UnitOfWork.ProductRepository.Update(product);
            UnitOfWork.Save();
            
            var manager = UnitOfWork.ManagerRepository.GetAll()
                .FirstOrDefault(x => x.LastName == purchaseDto.ManagerName);
            if (manager == null)
            {
                manager = Mapper.Map<PurchaseDto, Manager>(purchaseDto);
                manager.Products.Add(product);
                UnitOfWork.ManagerRepository.Create(manager);
            }

            return new OperationDetails(true, "successfull updated", "");
        }

        public OperationDetails Delete(int id)
        {
            UnitOfWork.ProductRepository.Delete(id);
            UnitOfWork.Save();
            return new OperationDetails(true, "successfull deleted", "");
        }

        private void InitializeMapper()
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
        }
    }
}