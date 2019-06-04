using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PageNotFound()
        {
            return this.View();
        }

        public IActionResult AlreadyExistsError(string error)
        {
            this.ViewData["Error"] = error;
            return this.View();
        }

        public IActionResult Invalid(string error)
        {
            this.ViewData["Error"] = error;
            return this.View();
        }

        public IActionResult ServerError()
        {
            return this.View();
        }
    }
}