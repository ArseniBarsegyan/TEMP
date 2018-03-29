using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CTR.MVC.Models;

namespace CTR.MVC.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
