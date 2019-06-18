using AutoMapper;
using HotelManagement.DataModels;
using HotelManagement.ViewModels;

namespace HotelManagement.Infrastructure.Mappings
{
    public class UserToUserViewModel : Profile
    {
        public UserToUserViewModel()
        {
            this.CreateMap<User, UserViewModel>()
              .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
              .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.UserName))
              .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
              .ForMember(dest => dest.LogbookManagers, opts => opts.MapFrom(src => src.LogbookManagers))
              .ForMember(dest => dest.Notes, opts => opts.MapFrom(src => src.Notes))
              .ReverseMap();
        }
    }
}