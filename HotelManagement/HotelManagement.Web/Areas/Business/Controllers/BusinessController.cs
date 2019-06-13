using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Services.Contracts;
using HotelManagement.ViewModels;
using HotelManagement.ViewModels.PublicArea;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

            return View(business);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comment(AddFeedbackViewModel model)
        {
            var feedbackModel = await this.feedbackService.AddComment(model);

            return this.PartialView("_CommentSectionPartial", feedbackModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReply(AddFeedbackViewModel model)
        {
            var feedbackModel = await this.feedbackService.AddReply(model);

            return this.PartialView("_ReplySectionPartial", feedbackModel);
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public async Task<IActionResult> DeleteFeedback(string data)
        {
            if (this.ModelState.IsValid)
            {
                await this.feedbackService.DeleteCommentAsync(data);

                return this.StatusCode(200);
            }

            return this.BadRequest();
        }
    }
}