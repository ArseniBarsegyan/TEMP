using System;
using AutoMapper;
using CTR.BLL.Dto;
using CTR.DAL.Entities;
using CTR.MVC.Models;

namespace CTR.MVC.Helpers
{
    /// <summary>
    /// Provides mapping from ViewModels to Dtos
    /// </summary>
    public static class MapperHelper
    {
        public static ActivityViewModel MapActivityDtoToActivityViewModel(ActivityDto dto)
        {
            return Mapper.Map<ActivityDto, ActivityViewModel>(dto);
        }

        //Create two-way mapping
        public static ActivityDto MapCreateViewModelToActivityDto(ActivityCreateViewModel model)
        {
            return Mapper.Map<ActivityCreateViewModel, ActivityDto>(model);
        }

        public static ActivityCreateViewModel MapActivityDtoToCreateViewModel(ActivityDto dto)
        {
            return Mapper.Map<ActivityDto, ActivityCreateViewModel>(dto);
        }

        //Update two-way mapping
        public static ActivityViewModel MapActivityUpdateDtoToActivityViewModel(ActivityUpdateDto dto)
        {
            return Mapper.Map<ActivityUpdateDto, ActivityViewModel>(dto);
        }

        public static ActivityUpdateDto MapActivityViewModelToActivityUpdateDto(ActivityViewModel model)
        {
            return Mapper.Map<ActivityViewModel, ActivityUpdateDto>(model);
        }

        /// <summary>
        /// Configure mapper options here
        /// </summary>
        public static void InitializeMapper()
        {
            Mapper.Initialize(cfg =>
            {
                //Create view model mapping
                cfg.CreateMap<ActivityDto, ActivityCreateViewModel>();
                cfg.CreateMap<ActivityCreateViewModel, ActivityDto>()
                    .ForMember(x => x.Date, opt => opt.UseValue(DateTime.Now));

                //Update view model mapping
                cfg.CreateMap<ActivityUpdateDto, ActivityViewModel>();
                cfg.CreateMap<ActivityViewModel, ActivityUpdateDto>();

                //For list of view models
                cfg.CreateMap<ActivityDto, ActivityViewModel>();
                cfg.CreateMap<ActivityViewModel, ActivityDto>();


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