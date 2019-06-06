using HotelManagement.Data;
using HotelManagement.Infrastructure;
using HotelManagement.Services.Contracts;
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

    }
}
