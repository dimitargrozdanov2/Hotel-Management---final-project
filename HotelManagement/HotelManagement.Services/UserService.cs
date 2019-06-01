using HotelManagement.Data;
using HotelManagement.Infrastructure;
using HotelManagement.Services.Contracts;
using HotelManagement.Services.Wrappers.Contracts;
using HotelManagement.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;
        private IMappingProvider mappingProvider;
        private IUserManagerWrapper userManagerWrapper;

        public UserService(ApplicationDbContext context, IMappingProvider mappingProvider, IUserManagerWrapper userManagerWrapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mappingProvider = mappingProvider ?? throw new ArgumentNullException(nameof(mappingProvider));
            this.userManagerWrapper = userManagerWrapper ?? throw new ArgumentNullException(nameof(userManagerWrapper));
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsers()
        {
            var users = await this.context.Users
                .Include(lb => lb.LogbookManagers)
                .Include(n => n.Notes)
                    .ThenInclude(l => l.Logbook)
                .Include(n => n.Notes)
                    .ThenInclude(c => c.Category)
                .ToListAsync();

            var returnUsers = this.mappingProvider.MapTo<ICollection<UserViewModel>>(users);

            return returnUsers;
        }
    }
}
