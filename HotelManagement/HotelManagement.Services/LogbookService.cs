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
    public class LogbookService : ILogbookService
    {
        private readonly ApplicationDbContext context;
        private readonly IMappingProvider mappingProvider;

        public LogbookService(ApplicationDbContext context, IMappingProvider mappingProvider)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mappingProvider = mappingProvider ?? throw new ArgumentNullException(nameof(mappingProvider));
        }

        public async Task<IEnumerable<LogbookViewModel>> GetLogBooksForBusiness(string name)
        {
            var logbooks = await this.context.Logbooks
               .Include(lb => lb.LogbookManagers)
               .Include(lb => lb.Business)
               .Where(lb => lb.Business.Name == name)
               .ToListAsync();

            var returnLogbooks = this.mappingProvider.MapTo<ICollection<LogbookViewModel>>(logbooks);

            return returnLogbooks;
        }

        public async Task<LogbookViewModel> CreateLogbookAsync(string businessname, string name, string description)
        {
            var business = await this.context.Businesses
                .Include(bu => bu.BusinessUnits)
                .Include(i => i.Images)
                .Include(f => f.Feedback)
                    .ThenInclude(r => r.Replies)
                .FirstOrDefaultAsync(b => b.Name == businessname);

            if (business == null)
            {
                throw new EntityInvalidException($"Business `{businessname}` does not exist.");
            }

            if (business.BusinessUnits.Any(bu => bu.Name == name))
            {
                throw new EntityAlreadyExistsException($"Logbook '{name}' already exist in Business '{businessname}'!");
            }

            var logbook = new Logbook() { Name = name, Description = description, Business = business };

            await this.context.Logbooks.AddAsync(logbook);
            await this.context.SaveChangesAsync();

            var returnLogbook = this.mappingProvider.MapTo<LogbookViewModel>(logbook);

            return returnLogbook;
        }



    }
}
