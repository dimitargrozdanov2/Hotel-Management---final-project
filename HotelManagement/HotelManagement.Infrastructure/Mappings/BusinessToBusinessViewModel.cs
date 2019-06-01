using AutoMapper;
using HotelManagement.DataModels;
using HotelManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.Infrastructure.Mappings
{
    public class BusinessToBusinessViewModel : Profile
    {
        public BusinessToBusinessViewModel()
        {
            this.CreateMap<Business, BusinessViewModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Location, opts => opts.MapFrom(src => src.Location))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(dest => dest.CreatedOn, opts => opts.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.ModifiedOn, opts => opts.MapFrom(src => src.ModifiedOn))
                .ForMember(dest => dest.BusinessUnits, opts => opts.MapFrom(src => src.BusinessUnits))
                .ForMember(dest => dest.Feedback, opts => opts.MapFrom(src => src.Feedback))
                .ForMember(dest => dest.Images, opts => opts.MapFrom(src => src.Images))
                .ReverseMap();
        }
    }
}
