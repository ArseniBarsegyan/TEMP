using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CTR.BLL.Dto;
using CTR.BLL.Helpers;
using CTR.BLL.Infrastructure;
using CTR.BLL.Interfaces;
using CTR.BLL.Util;
using CTR.DAL.Entities;
using CTR.DAL.Interfaces;

namespace CTR.BLL.Services
{
    /// <inheritdoc />
    /// <summary>
    /// Implementaion of IActivityService
    /// </summary>
    public class ActivityService : IActivityService
    {
        private readonly IGenericRepository<ActivityEntity> _repository;

        public ActivityService(IGenericRepository<ActivityEntity> repository)
        {
            _repository = repository;
        }

        public IQueryable<ActivityDto> GetAllActivitiesByDate(DateTime currentDate)
        {
            var allActivities = _repository.GetAll().Where(x => x.Date.Day == currentDate.Day && x.Date.Month == currentDate.Month && x.Date.Year == currentDate.Year);
            return MapActivityEntitiesToDto(allActivities).AsQueryable();
        }

        /// <summary>
        /// Map all dto data to entity and update database
        /// </summary>
        public async Task<OperationDetails> UpdateActivity(ActivityUpdateDto dto)
        {
            var entity = MapperHelper.MapActivityUpdateDtoToEntity(dto);
            _repository.Update(entity);
            await _repository.SaveAsync();
            return new OperationDetails(true, ConstantStore.ActivityUpdated, string.Empty);
        }

        private IList<ActivityDto> MapActivityEntitiesToDto(IEnumerable<ActivityEntity> activityEntities)
        {
            return activityEntities.Select(MapperHelper.MapActivityEntityToDto).ToList();
        }

        /// <summary>
        /// Get ActivityUpdateDto from ActivityEntity from Database
        /// </summary>
        public async Task<ActivityUpdateDto> GetActivityUpdateDtoByIdAsync(int? id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return MapperHelper.MapActivityEntityToActivityUpdateDto(entity);
        }

        /// <summary>
        /// Create activity from ActivityDto
        /// </summary>
        public async Task<OperationDetails> CreateActivityAsync(ActivityDto dto)
        {
            var activityEntity = MapperHelper.MapActivityDtoToEntity(dto);
            await _repository.CreateAsync(activityEntity);
            await _repository.SaveAsync();
            return new OperationDetails(true, ConstantStore.ActivityCreated, string.Empty);
        }

        /// <summary>
        /// Delete activity by id
        /// </summary>
        public async Task<OperationDetails> DeleteActivityAsync(int? id)
        {
            if (id == null)
                return new OperationDetails(false, ConstantStore.IncorrectParameter, string.Empty);
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
            return new OperationDetails(true, ConstantStore.ActivityDeleted, string.Empty);
        }
    }
}