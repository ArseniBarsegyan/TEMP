using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserStore.BLL.Interfaces;
using UserStore.BLL.Services;
using UserStore.DAL.Repositories;

namespace UserStore.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class ManagerController : Controller
    {
        private IManagerService _managerService = new ManagerService(new IdentityUnitOfWork("DefaultConnection"));

        public ActionResult Index()
        {
            var allManagers = _managerService.GetAllManagersList();
            return View(allManagers);
        }
    }
}