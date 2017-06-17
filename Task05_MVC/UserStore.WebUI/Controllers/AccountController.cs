using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using UserStore.BLL.Interfaces;

namespace UserStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IUserService UserService => HttpContext.GetOwinContext().GetUserManager<IUserService>();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}