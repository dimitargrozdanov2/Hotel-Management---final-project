using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.Services.Contracts;
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

        public BusinessService(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Business> GetBusinessByNameAsync(string name)
        {
            var business = await this.context.Businesses
                .Include(bu => bu.BusinessUnits)
                .Include(i => i.Images)
                .Include(f => f.Feedback)
                .FirstOrDefaultAsync(b => b.Name == name);

            return business;
        }

        public async Task<ICollection<Business>> GetBusinesses(string key, string location = null)
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

            return businesses;
        }
    }
}
