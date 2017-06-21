using System.Web.Mvc;
using UserStore.BLL.Interfaces;
using UserStore.BLL.Services;
using UserStore.DAL.Repositories;

namespace UserStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IOrderService _orderService = new OrderService(new UnitOfWork("DefaultConnection"));

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OrdersList()
        {
            var allOrders = _orderService.GetAllOrderList();
            return PartialView(allOrders);
        }
    }
}