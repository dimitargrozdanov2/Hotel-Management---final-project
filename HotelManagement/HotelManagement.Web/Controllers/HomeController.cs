using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelManagement.Web.Models;
using HotelManagement.Services.Contracts;

namespace HotelManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly INoteService noteService;

        public HomeController(INoteService noteService)
        {
            this.noteService = noteService ?? throw new ArgumentNullException(nameof(noteService));
        }

        public IActionResult Index()
        {
            var notes = this.noteService.GetNotes();
            return View();
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
