﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UserStore.BLL.DTO;
using UserStore.BLL.Infrastructure;
using UserStore.BLL.Interfaces;
using UserStore.DAL.Entities;
using UserStore.DAL.Interfaces;

namespace UserStore.BLL.Services
{
    public class ProductService : IProductService
    {
        private IUnitOfWork UnitOfWork { get; }

        public ProductService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IEnumerable<ProductDto> GetAllProductsList()
        {
            var productList = new List<ProductDto>();

            foreach (var product in UnitOfWork.ProductRepository.GetAll())
            {
                var productDto = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price
                };
                productList.Add(productDto);
            }
            return productList;
        }

        public ProductDto GetProductById(int id)
        {
            var product = UnitOfWork.ProductRepository.GetById(id);
            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
            return productDto;
        }

        public OperationDetails Create(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price
            };
            UnitOfWork.ProductRepository.Create(product);
            UnitOfWork.Save();

            return new OperationDetails(true, "product create successful", "");
        }

        public OperationDetails Edit(ProductDto productDto)
        {
            var product = UnitOfWork.ProductRepository.GetById(productDto.Id);
            product.Name = productDto.Name;
            product.Price = productDto.Price;
            UnitOfWork.ProductRepository.Update(product);
            UnitOfWork.Save();

            return new OperationDetails(true, "product update successful", "");
        }

        public OperationDetails Delete(int id)
        {
            //var product = UnitOfWork.ProductRepository.GetAll().FirstOrDefault(x => x.Id == id);
            //var order = UnitOfWork.OrderRepository.GetAll()
            //    .Include(x => x.Product)
            //    .FirstOrDefault(x => x.Product.Id == id);
            //if (order != null)
            //{
            //    UnitOfWork.OrderRepository.Delete(order.Id);
            //}
            UnitOfWork.ProductRepository.Delete(id);
            UnitOfWork.Save();

            return new OperationDetails(true, "product delete successful", "");
        }
    }
}