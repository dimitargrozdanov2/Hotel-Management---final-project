using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.Infrastructure;
using HotelManagement.Services.Contracts;
using HotelManagement.Services.Exceptions;
using HotelManagement.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ICollection<string>> GetAllCategoryNamesAsync(string logbookName)
        {
            var logbook = await this.context.Logbooks
                .Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.Name == logbookName);

            if(logbook == null)
            {
                throw new EntityInvalidException("Logbook has not been found!");
            }

            var categories = logbook.Categories.Select(x => x.Name).ToList();

            if (categories == null)
            {
                throw new EntityInvalidException("This logbook has no categories!");
            }

            return categories;
        }

        public async Task<CategoryViewModel> CreateCategoryAsync(string categoryName, string logbookName)
        {
            var logbook = await this.context.Logbooks
                .Include(l => l.Categories)
                .FirstOrDefaultAsync(l => l.Name == logbookName);

            if (logbook == null)
            {
                throw new EntityInvalidException($"Logbook `{logbookName}` has not been found!");
            }

            if (logbook.Categories.Any(m => m.Name == categoryName))
            {
                throw new EntityAlreadyExistsException($"Category '{categoryName}' already exists in Logbook '{logbookName}'!");
            }

            var category = new Category() { Name = categoryName };

            logbook.Categories.Add(category);

            await this.context.SaveChangesAsync();

            var returnCategory= this.mappingProvider.MapTo<CategoryViewModel>(category);

            return returnCategory;

        }
    }
}
