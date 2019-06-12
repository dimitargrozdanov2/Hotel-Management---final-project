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
using Newtonsoft.Json;

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

            var userLogbooks = await this.userService.GetUserLogbooksAsync(email, specifiedLogbook);
            model.Logbooks = userLogbooks;
            if(specifiedLogbook == null)
            {
                //model.SpecifiedLogbook = userLogbooks.FirstOrDefault();
                model.SpecifiedLogbook = userLogbooks.FirstOrDefault(x => x.Notes.Count > 0);
            }
            else
            {
                model.SpecifiedLogbook = userLogbooks.FirstOrDefault(l => l.Name == specifiedLogbook);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateNote(string name)
        {
            var categories = await this.categoryService.GetAllCategoryNamesAsync();
            var userLogbooks = await this.userService.GetUserLogbookNamesAsync(name);

            var returnField = new { categories, userLogbooks };
            return Json(returnField);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNote(CreateNoteViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                //return this.RedirectToAction("Details", "Movie", new { id = movie.Name });

                var result = await this.noteService.CreateNoteAsync(model);

                return this.PartialView("_NotePartial", result);
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteNote(string data)
        {
            if (this.ModelState.IsValid)
            {
                await this.noteService.DeleteNoteAsync(data);

                return StatusCode(200);
            }

            return BadRequest();
        }
    }
}