﻿using HotelManagement.Data;
using HotelManagement.DataModels;
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
    public class FeedbackService : IFeedbackService
    {
        private readonly ApplicationDbContext context;
        private readonly IMappingProvider mappingProvider;

        public FeedbackService(ApplicationDbContext context, IMappingProvider mappingProvider)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mappingProvider = mappingProvider ?? throw new ArgumentNullException(nameof(mappingProvider));
        }

        public async Task<FeedbackViewModel> AddComment(string businessId, string authorName, string email, string comment)
        {
            var business = await this.context.Businesses.FirstOrDefaultAsync(b => b.Id == businessId);

            var feedback = new Feedback() { Name = authorName, BusinessId = businessId, Comment = comment, Email = email };
            this.context.Feedback.Add(feedback);
            await this.context.SaveChangesAsync();

            var returnBusiness = this.mappingProvider.MapTo<FeedbackViewModel>(feedback);

            return returnBusiness;
        }
    }
}