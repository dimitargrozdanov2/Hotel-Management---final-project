using HotelManagement.Services.Contracts;
using HotelManagement.ViewModels;
using HotelManagement.ViewModels.Management;
using HotelManagement.Web.Areas.Management.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace HotelManagement.Web.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize(Roles = "Manager")]
    public class ManagementController : Controller
    {
        private readonly IUserService userService;
        private readonly INoteService noteService;
        private readonly ICategoryService categoryService;

        public ManagementController(IUserService userService, INoteService noteService, 
            ICategoryService categoryService, IHubContext<NoteHub> hubContext)
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
            if (specifiedLogbook == null)
            {
                model.SpecifiedLogbook = userLogbooks.FirstOrDefault();
            }
            else
            {
                model.SpecifiedLogbook = userLogbooks.FirstOrDefault(l => l.Name == specifiedLogbook);
            }
            return this.View(model);
        }

        public IActionResult Search(string data)
        {
            return View();
        }

        public JsonResult GetNotesAsyncJson(string data, string userIdentity, string searchByValue)
        {
            var notes = this.noteService.SearchByTextAsync(data, userIdentity, searchByValue);
            var model = new SearchViewModel();
            model.Notes = notes;

            var hey = JsonConvert.SerializeObject(model.Notes); // testing if there is json self referencing loop

            return Json(model.Notes);
        }

        [HttpGet]
        public async Task<IActionResult> CreateNote(string logbookName)
        {
            var categories = await this.categoryService.GetAllCategoryNamesAsync(logbookName);

            var returnField = new { categories };
            return this.Json(returnField);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateNote(CreateNoteViewModel model)
        //{
        //    if (this.ModelState.IsValid)
        //    {
        //        //return this.RedirectToAction("Details", "Movie", new { id = movie.Name });

        //        var result = await this.noteService.CreateNoteAsync(model);

        //        return this.PartialView("_NotePartial", result);
        //    }

        //    return this.View(model);
        //}

        [HttpPost]
        public async Task<IActionResult> DeleteNote(string data)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    await this.noteService.DeleteNoteAsync(data);
                }
                catch (Exception ex)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, ex);
                }

                return this.StatusCode(200);
            }

            return this.BadRequest();
        }
    }
}