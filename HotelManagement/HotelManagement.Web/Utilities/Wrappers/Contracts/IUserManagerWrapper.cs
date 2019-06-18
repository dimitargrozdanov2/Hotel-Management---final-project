using HotelManagement.DataModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.Web.Utilities.Wrappers.Contracts
{
    public interface IUserManagerWrapper
    {
        Task<User> FindByNameAsync(string username);

        Task<IdentityResult> AddToRoleAsync(User user, string role);

        Task<IdentityResult> UpdateUserAsync(User user);

        Task<IList<string>> GetAllRoles(string userName);
    }
}