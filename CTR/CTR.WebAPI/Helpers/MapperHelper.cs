using AutoMapper;
using CTR.BLL.Dto;
using CTR.DAL.Entities;

namespace CTR.WebAPI.Helpers
{
    public class MapperHelper
    {
        /// <summary>
        /// Configure mapper options here
        /// </summary>
        public static void InitializeMapper()
        {
            Mapper.Initialize(cfg =>
            {
                //Configure mapping for BLL
                cfg.CreateMap<ActivityDto, ActivityEntity>()
                    .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Type.ToString()));

                cfg.CreateMap<ActivityEntity, ActivityDto>()
                    .ForMember(x => x.Type, opt => opt.MapFrom(src => src.Name));

                cfg.CreateMap<ActivityUpdateDto, ActivityEntity>()
                    .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Type.ToString()));

                cfg.CreateMap<ActivityEntity, ActivityUpdateDto>()
                    .ForMember(x => x.Type, opt => opt.MapFrom(src => src.Name));
            });
        }
    }
}