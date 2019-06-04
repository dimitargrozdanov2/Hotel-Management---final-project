using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Web.Areas.Business.Controllers
{
    [Area("Business")]
    public class BusinessController : Controller
    {
        private readonly IBusinessService businessService;

        public BusinessController(IBusinessService businessService)
        {
            this.businessService = businessService ?? throw new ArgumentNullException(nameof(businessService));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string name)
        {
            var business = await this.businessService.GetBusinessByNameAsync(name);
            return View(business);
        }
    }
}