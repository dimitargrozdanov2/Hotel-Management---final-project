﻿using AutoMapper;
using HotelManagement.DataModels;
using HotelManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.Infrastructure.Mappings
{
    public class LogbookToLogbookViewModel : Profile
    {
        public LogbookToLogbookViewModel()
        {
            this.CreateMap<Logbook, LogbookViewModel>()
              .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
              .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
              .ForMember(dest => dest.CreatedOn, opts => opts.MapFrom(src => src.CreatedOn))
              .ForMember(dest => dest.ModifiedOn, opts => opts.MapFrom(src => src.ModifiedOn))
              .ForMember(dest => dest.Business, opts => opts.MapFrom(src => src.Business))
              .ForMember(dest => dest.Notes, opts => opts.MapFrom(src => src.Notes))
              .ForMember(dest => dest.LogbookManagers, opts => opts.MapFrom(src => src.LogbookManagers))
              .ReverseMap();
        }
    }
}
