using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.ViewModels.Management
{
    public class CreateNoteViewModel
    {
        public IEnumerable<SelectListItem> CategoryList { get; set; }

        public string Text { get; set; }

        public string LogbookId { get; set; }

        public string Email { get; set; }

        public string CategoryName { get; set; }

        //public PriorityType PriorityType { get; set; } // take it from eat project first ever asp.net

    }
}
