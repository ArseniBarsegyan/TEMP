﻿using System.Web.Mvc;
using PagedList;
using UserStore.BLL.DTO;
using UserStore.BLL.Interfaces;
using UserStore.WebUI.ConstantStorage;

namespace UserStore.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class ManagerController : Controller
    {
        private IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        public ActionResult Index(int? page)
        {
            var pageSize = ConstantStorage.ConstantStorage.pageSize;
            var pageNumber = (page ?? 1);
            var allManagers = _managerService.GetAllManagersList();
            return View(allManagers.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ManagerDto managerDto)
        {
            if (ModelState.IsValid)
            {
                var operationDetails = _managerService.Create(managerDto);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index", "Manager");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(managerDto);
        }

        public ActionResult Edit(int id)
        {
            var managerDto = _managerService.GetManagerById(id);
            if (managerDto != null)
            {
                return View(managerDto);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(ManagerDto managerDto)
        {
            if (ModelState.IsValid)
            {
                var operationDetails = _managerService.Edit(managerDto);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(managerDto);
        }

        public ActionResult Delete(int id)
        {
            var purchaseDto = _managerService.GetManagerById(id);
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
                var operationDetails = _managerService.Delete(id);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View();
        }
    }
}