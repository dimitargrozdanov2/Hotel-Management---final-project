using HotelManagement.Data;
using HotelManagement.Infrastructure;
using HotelManagement.Services.Contracts;
using HotelManagement.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext context;
        private readonly IMappingProvider mappingProvider;

        public CategoryService(ApplicationDbContext context, IMappingProvider mappingProvider)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mappingProvider = mappingProvider ?? throw new ArgumentNullException(nameof(mappingProvider));
        }

        public async Task<ICollection<CategoryViewModel>> GetAllCategoriesAsync()
        {
            var categories = await this.context.Categories.ToListAsync();

            var mappedCategories = this.mappingProvider.MapTo<ICollection<CategoryViewModel>>(categories);

            return mappedCategories;
        }
    }
}
