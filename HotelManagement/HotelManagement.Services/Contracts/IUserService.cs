using HotelManagement.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();

        Task<IEnumerable<LogbookViewModel>> GetUserLogbooksAsync(string email);
    }
}