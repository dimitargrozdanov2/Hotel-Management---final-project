using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.Infrastructure;
using HotelManagement.Services.Contracts;
using HotelManagement.Services.Exceptions;
using HotelManagement.ViewModels;
using HotelManagement.ViewModels.PublicArea;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<FeedbackViewModel> AddComment(AddFeedbackViewModel model)
        {
            var business = await this.context.Businesses.FirstOrDefaultAsync(b => b.Id == model.BusinessId);

            if (business == null)
            {
                throw new ArgumentException($"The business has not been found!");
            }

            var feedback = new Feedback()
            {
                Name = model.AuthorName,
                BusinessId = model.BusinessId,
                Comment = model.Comment,
                Email = model.Email
            };

            this.context.Feedback.Add(feedback);
            await this.context.SaveChangesAsync();

            var returnBusiness = this.mappingProvider.MapTo<FeedbackViewModel>(feedback);

            return returnBusiness;
        }

        public async Task<FeedbackViewModel> AddReply(AddFeedbackViewModel model)
        {
            var business = await this.context.Businesses.FirstOrDefaultAsync(b => b.Id == model.BusinessId);

            if (business == null)
            {
                throw new ArgumentException($"The business has not been found!");
            }

            var feedback = new Feedback()
            {
                Name = model.AuthorName,
                FeedbackParentId = model.FeedbackParentId,
                Comment = model.Comment,
                Email = model.Email
            };

            this.context.Feedback.Add(feedback);
            await this.context.SaveChangesAsync();

            var returnBusiness = this.mappingProvider.MapTo<FeedbackViewModel>(feedback);

            return returnBusiness;
        }

        public async Task<string> DeleteCommentAsync(string id)
        {
            var feedback = await this.context.Feedback
                .Include(r => r.Replies) // Including replies because they will be removed with RemoveRange when the comment is deleted
                .FirstOrDefaultAsync(l => l.Id == id);

            if (feedback == null)
            {
                throw new EntityInvalidException($"Feedback with id `{id}` has not been found!");
            }

            this.context.Feedback.RemoveRange(feedback.Replies);
            this.context.Feedback.Remove(feedback);
            await this.context.SaveChangesAsync();

            return id;
        }
    }
}
