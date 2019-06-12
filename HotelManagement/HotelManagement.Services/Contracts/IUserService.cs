using HotelManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsers();

        Task<UserViewModel> GetUserAsync(string email);

        Task<IEnumerable<LogbookViewModel>> GetUserLogbooksAsync(string email, string specifiedLogbook);

        Task<IEnumerable<string>> GetUserLogbookNamesAsync(string email);
    }
}
