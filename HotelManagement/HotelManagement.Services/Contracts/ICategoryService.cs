using HotelManagement.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.Services.Contracts
{
    public interface ICategoryService
    {
        Task<ICollection<string>> GetAllCategoryNamesAsync(string logbookName);

        Task<CategoryViewModel> CreateCategoryAsync(string categoryName, string logbookName);
    }
}