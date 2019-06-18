using HotelManagement.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.Services.Contracts
{
    public interface IBusinessService
    {
        Task<BusinessViewModel> GetBusinessByNameAsync(string name);

        Task<ICollection<BusinessViewModel>> GetBusinesses(string key, bool isDescending = true);

        Task<BusinessViewModel> CreateBusinessAsync(string name, string location, string description);

        Task<BusinessViewModel> AddImageToBusiness(string name, string imageUrl, IFormFile Image);
    }
}