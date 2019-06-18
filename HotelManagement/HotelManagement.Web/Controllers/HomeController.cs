using HotelManagement.Services.Contracts;
using HotelManagement.Services.Wrappers.Contracts;
using HotelManagement.Web.Models;
using HotelManagement.Web.Models.HomeViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

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
            var model = new HomeIndexViewModel();

            var business = await this.businessService.GetBusinesses("date", true);

            model.Businesses = business;

            return this.View(model);
        }

        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}