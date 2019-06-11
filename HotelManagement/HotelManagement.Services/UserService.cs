using HotelManagement.Data;
using HotelManagement.Infrastructure;
using HotelManagement.Services.Contracts;
using HotelManagement.Services.Wrappers.Contracts;
using HotelManagement.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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

        public async Task<UserViewModel> GetUserAsync(string email)
        {
            var user = await this.context.Users
                .Include(lb => lb.LogbookManagers)
                .Include(n => n.Notes)
                    .ThenInclude(l => l.Logbook)
                .Include(n => n.Notes)
                    .ThenInclude(c => c.Category)
                .FirstOrDefaultAsync(u => u.Email == email);

            var mappedUser = this.mappingProvider.MapTo<UserViewModel>(user);

            return mappedUser;
        }

        public async Task<IEnumerable<LogbookViewModel>> GetUserLogbooksAsync(string email)
        {
            //.Include(lb => lb.LogbookManagers)
            //.Include(n => n.Notes)
            //    .ThenInclude(l => l.Logbook)
            //.Include(n => n.Notes)
            //    .ThenInclude(c => c.Category)
            //.FirstOrDefaultAsync(u => u.Email == email);
            var logbooks = await this.context.Logbooks
                .Include(n => n.Notes)
                    .ThenInclude(l => l.Category)
                .Include(lb => lb.LogbookManagers)
                    .ThenInclude(m => m.Manager)
                .Include(b => b.Business)
                .Where(s => s.LogbookManagers.Any(x => x.Manager.Email == email))
                .ToListAsync();

            var mappedLogbooks = this.mappingProvider.MapTo<IEnumerable<LogbookViewModel>>(logbooks);

            return mappedLogbooks;
        }
    }
}
