﻿using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.Infrastructure;
using HotelManagement.Services.Contracts;
using HotelManagement.Services.Exceptions;
using HotelManagement.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HotelManagement.Services.Extensions;
using Microsoft.AspNetCore.Http;

namespace HotelManagement.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly ApplicationDbContext context;
        private readonly IMappingProvider mappingProvider;
        private readonly Dictionary<string, Expression<Func<Business, object>>> orderByDictionary;

        public BusinessService(ApplicationDbContext context, IMappingProvider mappingProvider)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mappingProvider = mappingProvider ?? throw new ArgumentNullException(nameof(mappingProvider));
            this.orderByDictionary = AddOrderElements();
        }

        public async Task<BusinessViewModel> GetBusinessByNameAsync(string name)
        {
            var business = await this.context.Businesses
                .Include(bu => bu.BusinessUnits)
                .Include(i => i.Images)
                .Include(f => f.Feedback)
                    .ThenInclude(r => r.Replies)
                .FirstOrDefaultAsync(b => b.Name == name);

            if(business == null)
            {
                throw new EntityInvalidException($"Business `{name}` does not exist.");
            }

            var mappedBusiness = this.mappingProvider.MapTo<BusinessViewModel>(business);

            return mappedBusiness;
        }

        public async Task<BusinessViewModel> CreateBusinessAsync(string name, string location, string description)
        {
            if (await this.context.Businesses.AnyAsync(m => m.Name == name))
            {
                throw new EntityAlreadyExistsException($"Business with '{name}' name already exists!");
            }

            var business = new Business() { Name = name, Location = location, Description = description };

            await this.context.Businesses.AddAsync(business);
            await this.context.SaveChangesAsync();

            var returnBusiness = this.mappingProvider.MapTo<BusinessViewModel>(business);

            return returnBusiness;
        }

        public async Task<BusinessViewModel> AddImageToBusiness(string name, string imageUrl, IFormFile Image)
        {
            var business = await this.context.Businesses
                .Include(bu => bu.BusinessUnits)
                .Include(i => i.Images)
                .Include(f => f.Feedback)
                .FirstOrDefaultAsync(b => b.Name == name);

            if (business == null)
            {
                throw new EntityInvalidException($"Business `{name}` does not exist.");
            }
            if (!imageUrl.Contains("logo"))
            {
                int startIndex = imageUrl.IndexOf('_');
                string logbookName = imageUrl.Substring((startIndex + 1), (imageUrl.Length - 5 - startIndex));
                var logbook = await this.context.Logbooks.FirstOrDefaultAsync(b => b.Name == logbookName);

                if (logbook == null)
                {
                    throw new EntityInvalidException($"Logbook {logbookName} does not exist!");
                }
            }

            if (!Image.ContentType.Contains("image"))
            {
                throw new EntityInvalidException("Uploaded file must be of type image");
            }

            if (business.Images.Any(x => x.Name == imageUrl))
            {
                throw new EntityAlreadyExistsException($"Business '{name}' already has an image with this name!");
            }

            var image = new Image() { Name = imageUrl };

            business.Images.Add(image);

            await this.context.SaveChangesAsync();

            var returnBusiness = this.mappingProvider.MapTo<BusinessViewModel>(business);

            return returnBusiness;
        }

        public async Task<ICollection<BusinessViewModel>> GetBusinesses(string key, bool isDescending = true)
        {
            ICollection<Business> businesses = null;

            if (orderByDictionary.ContainsKey(key))
            {
                businesses = await this.context.Businesses
                    .Include(bu => bu.BusinessUnits)
                    .Include(f => f.Feedback)
                        .ThenInclude(r => r.Replies)
                    .Include(i => i.Images)
                    .OrderByWithDirection(orderByDictionary[key], isDescending)
                    .ToListAsync();
            }

            var mappedBusinesses = this.mappingProvider.MapTo<ICollection<BusinessViewModel>>(businesses);

            return mappedBusinesses;
        }

        private Dictionary<string, Expression<Func<Business, object>>> AddOrderElements()
        {
            var dictionary = new Dictionary<string, Expression<Func<Business, object>>>();

            Expression<Func<Business, object>> orderByName = (Business b) => b.Name;

            Expression<Func<Business, object>> orderByRating = (Business b) => b.Feedback.Sum(x => x.Rating);

            Expression<Func<Business, object>> orderByDate = (Business b) => b.CreatedOn;

            dictionary.Add("name", orderByName);
            dictionary.Add("rating", orderByName);
            dictionary.Add("date", orderByName);

            return dictionary;
        }
    }
}
