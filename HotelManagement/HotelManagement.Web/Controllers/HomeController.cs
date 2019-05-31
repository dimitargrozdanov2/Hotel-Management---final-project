using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelManagement.Web.Models;
using HotelManagement.Services.Contracts;
using HotelManagement.Web.Models.HomeViewModels;

namespace HotelManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBusinessService businessService;
        private readonly IUserService userService;

        public HomeController(IBusinessService businessService, IUserService userService)
        {
            this.businessService = businessService ?? throw new ArgumentNullException(nameof(businessService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<IActionResult> Index()
        {
            // FOR TESTING PURPOSES: TODO
            var model = new HomeIndexViewModel();

            var business = await this.businessService.GetBusinesses("name");
            var users = await this.userService.GetAllUsers();
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
