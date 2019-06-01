using AutoMapper;
using HotelManagement.DataModels;
using HotelManagement.DataModels.Enums;
using HotelManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.Infrastructure.Mappings
{
    public class NoteToNoteViewModel : Profile
    {
        public NoteToNoteViewModel()
        {
            this.CreateMap<Note, NoteViewModel>()
              .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
              .ForMember(dest => dest.Text, opts => opts.MapFrom(src => src.Text))
              .ForMember(dest => dest.CreatedOn, opts => opts.MapFrom(src => src.CreatedOn))
              .ForMember(dest => dest.ModifiedOn, opts => opts.MapFrom(src => src.ModifiedOn))
              .ForMember(dest => dest.Logbook, opts => opts.MapFrom(src => src.Logbook))
              .ForMember(dest => dest.Category, opts => opts.MapFrom(src => src.Category))
              .ForMember(dest => dest.User, opts => opts.MapFrom(src => src.User))
              .ForMember(dest => dest.PriorityType, opts => opts.MapFrom(src => src.PriorityType))
              .ReverseMap();
        }
    }
}
