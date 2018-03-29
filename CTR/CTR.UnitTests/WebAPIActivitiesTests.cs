using System;
using System.Collections.Generic;
using System.Linq;
using CTR.BLL.Dto;
using CTR.BLL.Interfaces;
using CTR.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CTR.UnitTests
{
    public class WebAPIActivitiesTests
    {
        [Fact]
        public void GetMethodReturnsListOfActivities()
        {
            var mock = new Mock<IActivityService>();
            mock.Setup(x => x.GetAllActivitiesByDate(DateTime.Now.Date)).Returns(GetTestDtos().AsQueryable);

            var activitiesController = new ActivitiesController(mock.Object);
            var activities = activitiesController.GetActivities();

            Assert.Equal(ActivityType.Reading, activities.ElementAt(0).Type);
        }

        [Fact]
        public async void GetActivityByIdReturnsNotFoundIfIdIsNull()
        {
            var mock = new Mock<IActivityService>();
            mock.Setup(x => x.GetAllActivitiesByDate(DateTime.Now)).Returns(GetTestDtos().AsQueryable);

            var activitiesController = new ActivitiesController(mock.Object);
            var result = await activitiesController.GetActivityById(null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void CreateActivityReturnsBadRequestIfParameterIsNull()
        {
            var mock = new Mock<IActivityService>();
            var activitiesController = new ActivitiesController(mock.Object);
            var result = await activitiesController.CreateActivity(null);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void DeleteActivityReturnsNotFoundIfIdIsNull()
        {
            var mock = new Mock<IActivityService>();
            mock.Setup(x => x.GetActivityUpdateDtoByIdAsync(null))
                .ReturnsAsync(GetTestDto(null));
            var activitiesController = new ActivitiesController(mock.Object);
            var result = await activitiesController.DeleteActivity(null);
            Assert.IsType<BadRequestResult>(result);
        }

        private ActivityUpdateDto GetTestDto(int? id)
        {
            var activity = new ActivityUpdateDto
            {
                Id = 0,
                Description = "test dto",
                Type = ActivityType.Talking,
                Date = new DateTime(2018, 3, 5),
                Duration = TimeSpan.FromHours(0.5)
            };
            return id == null ? null : activity;
        }

        private IEnumerable<ActivityDto> GetTestDtos()
        {
            var activities = new List<ActivityDto>
            {
                new ActivityDto
                {
                    Id = 0,
                    Description = "Test Dto 0",
                    Date = new DateTime(2018, 3, 5),
                    Type = ActivityType.Reading,
                    Duration = TimeSpan.FromHours(1)
                },
                new ActivityDto
                {
                    Id = 1,
                    Description = "Test Dto 1",
                    Date = new DateTime(2018, 2, 5),
                    Type = ActivityType.Learning,
                    Duration = TimeSpan.FromHours(2)
                }
            };
            return activities;
        }
    }
}
