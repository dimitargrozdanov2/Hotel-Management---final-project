using HotelManagement.DataModels;
using HotelManagement.Web.Utilities.Wrappers.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.Web.Utilities.Wrappers
{
    public class UserManagerWrapper : IUserManagerWrapper
    {
        private readonly UserManager<User> userManager;

        public UserManagerWrapper(UserManager<User> userManager)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<User> FindByNameAsync(string username)
        {
            return await this.userManager.FindByNameAsync(username);
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            return await this.userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await this.userManager.UpdateAsync(user);
        }

        public async Task<IList<string>> GetAllRoles(string userName)
        {
            User user = await this.userManager.FindByNameAsync(userName);

            return await this.userManager.GetRolesAsync(user);
        }
    }
}
