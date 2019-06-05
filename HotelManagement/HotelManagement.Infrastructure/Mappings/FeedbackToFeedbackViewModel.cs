using AutoMapper;
using HotelManagement.DataModels;
using HotelManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.Infrastructure.Mappings
{
    public class FeedbackToFeedbackViewModel : Profile
    {
        public FeedbackToFeedbackViewModel()
        {
            this.CreateMap<Feedback, FeedbackViewModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.CreatedOn, opts => opts.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.ModifiedOn, opts => opts.MapFrom(src => src.ModifiedOn))
                .ForMember(dest => dest.Comment, opts => opts.MapFrom(src => src.Comment))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.Rating, opts => opts.MapFrom(src => src.Rating))
                .ForMember(dest => dest.Replies, opts => opts.MapFrom(src => src.Replies))
                .ForMember(dest => dest.Business, opts => opts.MapFrom(src => src.Business))
                .ReverseMap();
        }
    }
}
