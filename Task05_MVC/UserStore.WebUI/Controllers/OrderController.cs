﻿using System.Linq;
using System.Web.Mvc;
using PagedList;
using UserStore.BLL.DTO;
using UserStore.BLL.Interfaces;
using UserStore.WebUI.Models;
using UserStore.WebUI.Util;

namespace UserStore.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public ActionResult Index(int? page)
        {
            var pageSize = ConstantStorage.pageSize;
            var pageNumber = (page ?? 1);
            var allOrders = _orderService.GetAllOrderList();
            return View(allOrders.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult OrderSearch(string name)
        {
            name = name.ToLower();
            var orders = _orderService.GetAllOrderList().Where(x => x.ManagerName.ToLower() == name);
            ViewBag.Orders = orders;
            return PartialView(orders);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(OrderCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var orderDto = new OrderDto
                {
                    ClientName = model.ClientName,
                    ManagerName = model.ManagerName,
                    Price = model.Price,
                    ProductName = model.ProductName,
                    Date = model.Date
                };

                var operationDetails = _orderService.Create(orderDto);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index", "Order");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var purchaseDto = _orderService.GetOrderDtoById(id);
            if (purchaseDto != null)
            {
                return View(purchaseDto);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(OrderDto orderDto)
        {
            if (ModelState.IsValid)
            {
                var operationDetails = _orderService.Edit(orderDto);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(orderDto);
        }

        public ActionResult Delete(int id)
        {
            var purchaseDto = _orderService.GetOrderDtoById(id);
            if (purchaseDto != null)
            {
                return View(purchaseDto);
            }
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                var operationDetails = _orderService.Delete(id);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View();
        }
    }
}