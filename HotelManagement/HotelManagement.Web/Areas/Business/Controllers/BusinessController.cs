using HotelManagement.Services.Contracts;
using HotelManagement.ViewModels.PublicArea;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HotelManagement.Web.Areas.Business.Controllers
{
    [Area("Business")]
    public class BusinessController : Controller
    {
        private readonly IBusinessService businessService;
        private readonly IFeedbackService feedbackService;

        public BusinessController(IBusinessService businessService, IFeedbackService feedbackService)
        {
            this.businessService = businessService ?? throw new ArgumentNullException(nameof(businessService));
            this.feedbackService = feedbackService ?? throw new ArgumentNullException(nameof(feedbackService));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string name)
        {
            var business = await this.businessService.GetBusinessByNameAsync(name);

            return this.View(business);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comment(AddFeedbackViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    var feedbackModel = await this.feedbackService.AddComment(model);
                    return this.PartialView("_CommentSectionPartial", feedbackModel);
                }
                catch (Exception ex)
                {

                    return this.StatusCode((int)HttpStatusCode.InternalServerError, ex);
                }
            }

            return this.BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReply(AddFeedbackViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    var feedbackModel = await this.feedbackService.AddReply(model);

                    return this.PartialView("_ReplySectionPartial", feedbackModel);
                }
                catch (Exception ex)
                {
                    return this.StatusCode((int)HttpStatusCode.InternalServerError, ex);
                }
            }
            return this.BadRequest();
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public async Task<IActionResult> DeleteFeedback(string data)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    await this.feedbackService.DeleteCommentAsync(data);
                }
                catch (Exception ex)
                {
                    return this.StatusCode((int)HttpStatusCode.InternalServerError, ex);
                }

                return this.StatusCode(200);
            }

            return this.BadRequest();
        }
    }
}