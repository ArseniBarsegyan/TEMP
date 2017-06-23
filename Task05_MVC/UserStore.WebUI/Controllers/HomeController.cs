using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UserStore.BLL.Interfaces;
using UserStore.BLL.Services;
using UserStore.DAL.Repositories;
using UserStore.WebUI.Models;

namespace UserStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IOrderService _orderService = new OrderService(new UnitOfWork("DefaultConnection"));
        public int PageSize = 2;

        public ViewResult List(int page = 1)
        {
            var ordersListViewModel = new OrderListViewModel
            {
                Orders = _orderService.GetAllOrderList()
                    .OrderBy(x => x.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _orderService.GetAllOrderList().Count()
                }
            };
            return View(ordersListViewModel);
        }

        public ActionResult Index(string manager, string product, string date)
        {
            var orders = _orderService.GetAllOrderList();

            var managers = orders.Select(x => x.ManagerName).Distinct().ToList();
            managers.Insert(0, "All");
            var products = orders.Select(x => x.ProductName).Distinct().ToList();
            products.Insert(0, "All");
            var dates = orders.Select(x => x.Date.ToString("d")).Distinct().ToList();
            dates.Insert(0, "All");
            
            var prices = orders.Select(x => x.Price).Distinct().ToList();
            prices.Insert(0, 0m);

            if (!string.IsNullOrEmpty(manager) && !manager.Equals("All"))
            {
                orders = orders.Where(x => x.ManagerName == manager);
            }

            if (!string.IsNullOrEmpty(product) && !product.Equals("All"))
            {
                orders = orders.Where(x => x.ProductName == product);
            }

            if (!string.IsNullOrEmpty(date) && !date.Equals("All"))
            {
                orders = orders.Where(x => x.Date.ToString("d") == date);
            }

            var ordersListViewModel = new OrderListViewModel
            {
                Orders = orders,
                Managers = new SelectList(managers),
                Products = new SelectList(products),
                Dates = new SelectList(dates),
            };

            return View(ordersListViewModel);
        }

        [HttpPost]
        public ActionResult OrdersList()
        {
            var allOrders = _orderService.GetAllOrderList();
            
            ViewBag.Managers = allOrders.Select(order => order.ManagerName);
            ViewBag.Products = allOrders.Select(order => order.ProductName);
            ViewBag.Dates = allOrders.Select(order => order.Date);

            return PartialView(allOrders);
        }
    }
}