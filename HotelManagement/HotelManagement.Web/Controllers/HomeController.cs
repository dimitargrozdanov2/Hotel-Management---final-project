using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelManagement.Web.Models;
using HotelManagement.Services.Contracts;
using HotelManagement.Web.Models.HomeViewModels;
using HotelManagement.Web.Utilities.Wrappers.Contracts;

namespace HotelManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBusinessService businessService;
        private readonly IUserService userService;
        private readonly IUserManagerWrapper userManagerWrapper;

        public HomeController(IBusinessService businessService, IUserService userService, IUserManagerWrapper userManagerWrapper)
        {
            this.businessService = businessService ?? throw new ArgumentNullException(nameof(businessService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.userManagerWrapper = userManagerWrapper ?? throw new ArgumentNullException(nameof(userManagerWrapper));
        }

        public async Task<IActionResult> Index()
        {
            // FOR TESTING PURPOSES: TODO
            var model = new HomeIndexViewModel();

            var business = await this.businessService.GetBusinesses("name");
            var users = await this.userService.GetAllUsers();

            var roles = await this.userManagerWrapper.GetAllRoles("admin@admin.admin");

            model.Businesses = business;

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        
    }
}
