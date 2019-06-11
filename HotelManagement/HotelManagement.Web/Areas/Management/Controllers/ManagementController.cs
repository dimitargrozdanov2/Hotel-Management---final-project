using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Services.Contracts;
using HotelManagement.ViewModels;
using HotelManagement.ViewModels.Management;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagement.Web.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize(Roles ="Manager")]
    public class ManagementController : Controller
    {
        private readonly IUserService userService;
        private readonly INoteService noteService;
        private readonly ICategoryService categoryService;

        public ManagementController(IUserService userService, INoteService noteService, ICategoryService categoryService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.noteService = noteService ?? throw new ArgumentNullException(nameof(noteService));
            this.categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
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

        //[HttpGet]
        //public async Task<IActionResult> CreateNote()
        //{
        //    var model = new CreateNoteViewModel();
        //    var categories = await this.categoryService.GetAllCategoriesAsync();
        //    model.CategoryList = categories.Select(t => new SelectListItem(t.Name, t.Name)).ToList();

        //    return this.View(model);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateNote(CreateNoteViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.noteService.CreateNoteAsync(model);
                //return this.RedirectToAction("Details", "Movie", new { id = movie.Name });
                return null;
            }

            return this.View(model);
        }
    }
}