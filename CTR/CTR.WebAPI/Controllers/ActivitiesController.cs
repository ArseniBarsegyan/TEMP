using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CTR.BLL.Dto;
using CTR.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CTR.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ActivitiesController : Controller
    {
        private readonly IActivityService _activityService;

        public ActivitiesController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpGet]
        public IEnumerable<ActivityDto> GetActivities()
        {
            return _activityService.GetAllActivitiesByDate(DateTime.Now.Date);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityById(int? id)
        {
            var activity = await _activityService.GetActivityUpdateDtoByIdAsync(id);
            if (activity == null)
                return NotFound();
            return new ObjectResult(activity);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody]ActivityDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            var operationDetails = await _activityService.CreateActivityAsync(dto);
            if (operationDetails.Succedeed)
                return Ok();

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int? id)
        {
            if (id == null)
                return BadRequest();

            var operationDetails = await _activityService.DeleteActivityAsync(id);

            if (operationDetails.Succedeed)
                return Ok();

            return NotFound();
        }
    }
}
