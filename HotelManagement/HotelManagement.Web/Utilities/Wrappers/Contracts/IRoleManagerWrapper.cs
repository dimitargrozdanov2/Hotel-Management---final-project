using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Web.Utilities.Wrappers.Contracts
{
    public interface IRoleManagerWrapper
    {
        IQueryable<IdentityRole> GetAllRoles();

        Task<IdentityResult> CreateRoleAsync(string name);

        Task<IdentityRole> FindByNameAsync(string name);

        Task<IdentityResult> DeleteRoleAsync(string name);

        Task<IdentityResult> UpdateRoleAsync(IdentityRole role);
    }
}
