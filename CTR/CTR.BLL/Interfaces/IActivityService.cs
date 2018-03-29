using System;
using System.Linq;
using System.Threading.Tasks;
using CTR.BLL.Dto;
using CTR.BLL.Infrastructure;

namespace CTR.BLL.Interfaces
{
    /// <summary>
    /// Provides CRUD operations with Activities
    /// </summary>
    public interface IActivityService
    {
        Task<OperationDetails> CreateActivityAsync(ActivityDto dto);
        IQueryable<ActivityDto> GetAllActivitiesByDate(DateTime currentDate);
        Task<OperationDetails> UpdateActivity(ActivityUpdateDto dto);
        Task<OperationDetails> DeleteActivityAsync(int? id);
        Task<ActivityUpdateDto> GetActivityUpdateDtoByIdAsync(int? id);
    }
}