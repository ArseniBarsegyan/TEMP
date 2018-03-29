using AutoMapper;
using CTR.BLL.Dto;
using CTR.DAL.Entities;

namespace CTR.BLL.Helpers
{
    /// <summary>
    /// Provide mapping from Entities in DAL to DTO's in BLL and vice versa <para />
    /// Note: Configure mapper in client app
    /// </summary>
    public static class MapperHelper
    {
        public static ActivityDto MapActivityEntityToDto(ActivityEntity entity)
        {
            return Mapper.Map<ActivityEntity, ActivityDto>(entity);
        }

        public static ActivityEntity MapActivityDtoToEntity(ActivityDto dto)
        {
            return Mapper.Map<ActivityDto, ActivityEntity>(dto);
        }

        public static ActivityEntity MapActivityUpdateDtoToEntity(ActivityUpdateDto dto)
        {
            return Mapper.Map<ActivityUpdateDto, ActivityEntity>(dto);
        }

        public static ActivityUpdateDto MapActivityEntityToActivityUpdateDto(ActivityEntity entity)
        {
            return Mapper.Map<ActivityEntity, ActivityUpdateDto>(entity);
        }
    }
}