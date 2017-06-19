﻿using System.Web.Mvc;
using UserStore.BLL.DTO;
using UserStore.BLL.Interfaces;
using UserStore.BLL.Services;
using UserStore.DAL.Repositories;
using UserStore.WebUI.Models;

namespace UserStore.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class PurchaseController : Controller
    {
        private IPurchaseService _purchaseService = new PurchaseService(new IdentityUnitOfWork("DefaultConnection"));

        public ActionResult Index()
        {
            var allSales = _purchaseService.GetAllSalesList();
            return View(allSales);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PurchaseModel model)
        {
            if (ModelState.IsValid)
            {
                var purchaseDto = new PurchaseDto
                {
                    ClientName = model.ClientName,
                    ManagerName = model.ManagerName,
                    Price = model.Price,
                    ProductName = model.ProductName,
                    Date = model.Date
                };

                var operationDetails = _purchaseService.Create(purchaseDto);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index", "Purchase");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }
    }
}