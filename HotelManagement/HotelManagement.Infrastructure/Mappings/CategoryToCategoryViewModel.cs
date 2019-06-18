using AutoMapper;
using HotelManagement.DataModels;
using HotelManagement.ViewModels;

namespace HotelManagement.Infrastructure.Mappings
{
    public class CategoryToCategoryViewModel : Profile
    {
        public CategoryToCategoryViewModel()
        {
            this.CreateMap<Category, CategoryViewModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.CreatedOn, opts => opts.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.ModifiedOn, opts => opts.MapFrom(src => src.ModifiedOn))
                .ForMember(dest => dest.Notes, opts => opts.MapFrom(src => src.Notes))
                .ReverseMap();
        }
    }
}