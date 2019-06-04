using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Services.Contracts;
using HotelManagement.Web.Areas.Administration.Models.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class BusinessManagementController : Controller
    {
        private readonly IBusinessService businessService;

        public BusinessManagementController(IBusinessService businessService)
        {
            this.businessService = businessService ?? throw new ArgumentNullException(nameof(businessService));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new ListBusinessesViewModel();

            var businesses = await this.businessService.GetAllBusinessesAsync();

            model.Businesses = businesses;

            return this.View(model);

        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateBusinessViewModel();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBusinessViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var business = await this.businessService.CreateBusinessAsync(model.Name, model.Location, model.Description);
                return this.RedirectToAction("Index", "BusinessManagement");
            }

            return this.View(model);
        }
    }
}