﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Services.Contracts;
using HotelManagement.ViewModels.Management;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Web.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize(Roles ="Manager")]
    public class ManagementController : Controller
    {
        private readonly IUserService userService;

        public ManagementController(IUserService userService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<IActionResult> Index(string email, string specifiedLogbook)
        {
            var model = new ManagementIndexViewModel();

            var userLogbooks = await this.userService.GetUserLogbooksAsync(email);
            model.Logbooks = userLogbooks;
            if(specifiedLogbook == null)
            {
                model.SpecifiedLogbook = userLogbooks.FirstOrDefault();
            }
            else
            {
                model.SpecifiedLogbook = userLogbooks.FirstOrDefault(l => l.Name == specifiedLogbook);
            }
            return View(model);
        }
    }
}