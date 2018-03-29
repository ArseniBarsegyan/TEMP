using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CTR.BLL.Dto;
using CTR.BLL.Infrastructure;
using CTR.BLL.Interfaces;
using CTR.MVC.Helpers;
using CTR.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CTR.MVC.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new List<ActivityViewModel>();
            var activities = _activityService.GetAllActivitiesByDate(DateTime.Now);
            foreach (var activityDto in activities)
            {
                model.Add(MapperHelper.MapActivityDtoToActivityViewModel(activityDto));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActivityCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                ActivityDto activityDto = MapperHelper.MapCreateViewModelToActivityDto(model);
                activityDto.Date = DateTime.Now;

                OperationDetails operationDetails = await _activityService.CreateActivityAsync(activityDto);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            var updateDto = await _activityService.GetActivityUpdateDtoByIdAsync(id);
            if (updateDto != null)
            {
                ActivityViewModel model = MapperHelper.MapActivityUpdateDtoToActivityViewModel(updateDto);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(ActivityViewModel model)
        {
            if (ModelState.IsValid)
            {
                ActivityUpdateDto updateDto = MapperHelper.MapActivityViewModelToActivityUpdateDto(model);
                OperationDetails operationDetails = await _activityService.UpdateActivity(updateDto);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            ActivityUpdateDto dto = await _activityService.GetActivityUpdateDtoByIdAsync(id);
            if (dto != null)
            {
                ActivityViewModel model = MapperHelper.MapActivityUpdateDtoToActivityViewModel(dto);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (ModelState.IsValid)
            {
                OperationDetails operationDetails = await _activityService.DeleteActivityAsync(id);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index");
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View();
        }
    }
}