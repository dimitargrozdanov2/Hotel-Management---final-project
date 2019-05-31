using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Services.Contracts;
using HotelManagement.Web.Utilities.Wrappers.Contracts;
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

        public AdminController(IUserManagerWrapper userManagerWrapper,
            IUserService userService)
        {
            this.userManagerWrapper = userManagerWrapper;
            this.userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}