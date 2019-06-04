using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Services.Contracts;
using HotelManagement.Services.Wrappers.Contracts;
using HotelManagement.Web.Areas.Administration.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserManagerWrapper userManagerWrapper;
        private readonly IUserService userService;
        private readonly IBusinessService businessService;


        public AdminController(IUserManagerWrapper userManagerWrapper,
            IUserService userService, IBusinessService businessService)
        {
            this.userManagerWrapper = userManagerWrapper;
            this.userService = userService;
            this.businessService = businessService ?? throw new ArgumentNullException(nameof(businessService));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new IndexViewModel();

            var users = await this.userService.GetAllUsers();

            model.Users = users;

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AllBusinesses()
        {
            var model = new ListBusinessesViewModel();

            var businesses = await this.businessService.GetBusinesses("date");

            model.Businesses = businesses;

            return this.View(model);

        }

        [HttpGet]
        public IActionResult CreateBusiness()
        {
            var model = new CreateBusinessViewModel();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBusiness(CreateBusinessViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var business = await this.businessService.CreateBusinessAsync(model.Name, model.Location, model.Description);
                return this.RedirectToAction("AllBusinesses", "Admin");
            }

            return this.View(model);
        }
    }
}