using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.Infrastructure;
using HotelManagement.Services.Contracts;
using HotelManagement.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly ApplicationDbContext context;
        private readonly IMappingProvider mappingProvider;

        public BusinessService(ApplicationDbContext context, IMappingProvider mappingProvider)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mappingProvider = mappingProvider ?? throw new ArgumentNullException(nameof(mappingProvider));
        }

        public async Task<BusinessViewModel> GetBusinessByNameAsync(string name)
        {
            var business = await this.context.Businesses
                .Include(bu => bu.BusinessUnits)
                .Include(i => i.Images)
                .Include(f => f.Feedback)
                .FirstOrDefaultAsync(b => b.Name == name);

            var mappedBusiness = this.mappingProvider.MapTo<BusinessViewModel>(business);

            return mappedBusiness;
        }

        public async Task<ICollection<BusinessViewModel>> GetBusinesses(string key, string location = null)
        {
            ICollection<Business> businesses = null;

            if (key == "name")
            {
                businesses = await this.context.Businesses
                    .Include(bu => bu.BusinessUnits)
                    .Include(f => f.Feedback)
                    .Include(i => i.Images)
                    .OrderBy(k => k.Name)
                    .ToListAsync();

            }
            else if (key == "rating")
            {
                businesses = await this.context.Businesses
                    .Include(bu => bu.BusinessUnits)
                    .Include(i => i.Images)
                    .Include(f => f.Feedback)
                    .OrderByDescending(k => k.Feedback.Sum(x => x.Rating))
                    .ToListAsync();
            }
            else if (key == "date")
            {
                businesses = await this.context.Businesses
                    .Include(bu => bu.BusinessUnits)
                   .Include(i => i.Images)
                   .Include(f => f.Feedback)
                   .OrderBy(k => k.CreatedOn)
                   .ToListAsync();

            }
            else if (key == "location") // by country mostly
            {
                businesses = await this.context.Businesses
                    .Include(bu => bu.BusinessUnits)
                    .Include(i => i.Images)
                    .Include(f => f.Feedback)
                    .Where(l => l.Location.Contains(location))
                    .ToListAsync();
            }

            var mappedBusinesses = this.mappingProvider.MapTo<ICollection<BusinessViewModel>>(businesses);

            return mappedBusinesses;
        }
    }
}
