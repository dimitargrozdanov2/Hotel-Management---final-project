using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Services.Contracts;
using HotelManagement.ViewModels;
using HotelManagement.Web.Areas.Business.Models;
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
        public async Task<IActionResult> Comment(string id, CreateFeedbackViewModel model)
        {
            var opsa = await this.feedbackService.AddComment(model.BusinessId, model.AuthorName, model.Email, model.Comment);

            return this.PartialView("_CommentSectionPartial", opsa);
        }
    }
}