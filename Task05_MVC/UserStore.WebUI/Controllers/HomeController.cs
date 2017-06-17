using System.Web.Mvc;

namespace UserStore.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}