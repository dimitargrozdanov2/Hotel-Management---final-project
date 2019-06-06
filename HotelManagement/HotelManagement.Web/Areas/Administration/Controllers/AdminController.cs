using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Services.Contracts;
using HotelManagement.Services.Exceptions;
using HotelManagement.Services.Wrappers.Contracts;
using HotelManagement.Web.Areas.Administration.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ILogbookService logbookService;



        public AdminController(IUserManagerWrapper userManagerWrapper,
            IUserService userService, IBusinessService businessService, IHostingEnvironment hostingEnvironment, ILogbookService logbookService)
        {
            this.userManagerWrapper = userManagerWrapper;
            this.userService = userService;
            this.businessService = businessService ?? throw new ArgumentNullException(nameof(businessService));
            this.hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
            this.logbookService = logbookService ?? throw new ArgumentNullException(nameof(logbookService));

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

        [HttpGet]
        public  IActionResult AddImageToBusiness()
        {
            var model = new AddImageToBusinessViewModel();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImageToBusiness(AddImageToBusinessViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var imageNameToSave = model.ImageName + ".jpg";

                var business = await this.businessService.AddImageToBusiness(model.BusinessName, imageNameToSave, model.Image);

                using (var ms = new MemoryStream())
                {
                    model.Image.CopyTo(ms);
                    var uploads = Path.Combine(this.hostingEnvironment.WebRootPath, "images\\Project");
                    var filePath = Path.Combine(uploads, imageNameToSave);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(fileStream);
                    }
                }

                return this.RedirectToAction("AllBusinesses", "Admin");
                }

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AllLogbooksForBusiness(string name)
        {
            var model = new ListLogbooksViewModel();

            var logbooks = await this.logbookService.GetLogBooksForBusiness(name);

            model.Logbooks = logbooks;

            return this.View(model);
        }
    }
}