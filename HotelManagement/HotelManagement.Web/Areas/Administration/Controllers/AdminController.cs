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
using Microsoft.AspNetCore.Mvc.Rendering;

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
        private readonly IRoleManagerWrapper roleManagerWrapper;
        private readonly ICategoryService categoryService;

        public AdminController(IUserManagerWrapper userManagerWrapper,
            IUserService userService, IBusinessService businessService, IHostingEnvironment hostingEnvironment, 
            ILogbookService logbookService, IRoleManagerWrapper roleManagerWrapper, ICategoryService categoryService)
        {
            this.userManagerWrapper = userManagerWrapper;
            this.userService = userService;
            this.businessService = businessService ?? throw new ArgumentNullException(nameof(businessService));
            this.hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
            this.logbookService = logbookService ?? throw new ArgumentNullException(nameof(logbookService));
            this.roleManagerWrapper = roleManagerWrapper ?? throw new ArgumentNullException(nameof(roleManagerWrapper));
            this.categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new ListUsersViewModel();

            var users = await this.userService.GetAllUsers();

            model.Users = users;

            var allRoles = this.roleManagerWrapper.GetAllRoles();

            model.RoleList = allRoles.Select(r => new SelectListItem(r.Name, r.Name)).ToList();

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
            if (!this.ModelState.IsValid)
            {
                return BadRequest(model);
            }

            try
            {
                var business = await this.businessService.CreateBusinessAsync(model.Name, model.Location, model.Description);

                return Json(business);
            }
            catch (EntityAlreadyExistsException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult AddImageToBusiness(string name)
        {
            if (this.ModelState.IsValid)
            {
                var model = new AddImageToBusinessViewModel();
                model.name = name;
            }
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImageToBusiness(AddImageToBusinessViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var imageNameToSave = model.ImageName + ".jpg";

                var business = await this.businessService.AddImageToBusiness(model.name, imageNameToSave, model.Image);

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
            if (this.ModelState.IsValid)
            {
                var model = new ListLogbooksViewModel();

                var logbooks = await this.logbookService.GetLogBooksForBusiness(name);

                model.Logbooks = logbooks;

                model.BusinessName = name;

                return this.View(model);
            }
            return this.View();
        }

        [HttpGet]
        public IActionResult CreateLogbook()
        {
            var model = new CreateLogbookViewModel();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLogbook(string businessname, CreateLogbookViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var logbook = await this.logbookService.CreateLogbookAsync(businessname, model.Name, model.Description);
                return this.RedirectToAction("AllLogbooksForBusiness", "Admin", new { name = businessname });
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PromoteUser(PromoteRoleViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userManagerWrapper.FindByIdAsync(model.UserId);

                if (user == null)
                {
                    throw new EntityInvalidException("User not found!");
                }

                if ((await userManagerWrapper.GetAllRoles(user.UserName)).Contains(model.RoleName))
                {
                    var userAlreadyHasRoleMessage = $"{user.UserName} is already assigned role {model.RoleName}";
                    this.ModelState.AddModelError("Error", userAlreadyHasRoleMessage);
                    return BadRequest(new { message = userAlreadyHasRoleMessage });
                }
                await this.userManagerWrapper.AddToRoleAsync(user, model.RoleName);

                var promoteRoleViewModel = new PromoteRoleViewModel
                {
                    RoleName = model.RoleName,
                    UserId = user.Id
                };

                return Json(promoteRoleViewModel);
            }

            return BadRequest(model);
        }

        [HttpGet]
        public IActionResult ManageManager(string LogbookName)
        {
            if (this.ModelState.IsValid)
            {
                var model = new ManagerManageViewModel();
                model.LogbookName = LogbookName;

            }
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageManager(ManagerManageViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var logbook = await this.logbookService.ManageManagerAsync(model.LogbookName, model.ManagerEmail);

                return this.RedirectToAction("AllBusinesses", "Admin");
            }

            return this.View(model);
        }

        [HttpGet]
        public IActionResult CreateCategory(string LogbookName)
        {
            if (this.ModelState.IsValid)
            {
                var model = new CreateCategoryViewModel();
                model.LogbookName = LogbookName;
            }
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CreateCategoryViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.categoryService.CreateCategoryAsync(model.CategoryName, model.LogbookName);

                return this.RedirectToAction("AllBusinesses", "Admin");
            }

            return this.View(model);
        }

        [HttpGet]
        public IActionResult DeleteLogbook(string name)
        {
            if (this.ModelState.IsValid)
            {
                var model = new DeleteLogbookViewModel();
                model.Name = name;
            }
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLogbook(DeleteLogbookViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.logbookService.DeleteLogbook(model.Name);

                return this.RedirectToAction("AllBusinesses", "Admin");
            }

            return this.View();
        }
    }
}