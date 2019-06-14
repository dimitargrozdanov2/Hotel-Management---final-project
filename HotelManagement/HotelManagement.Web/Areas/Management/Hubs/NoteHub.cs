using HotelManagement.Services.Contracts;
using HotelManagement.ViewModels.Management;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Web.Areas.Management.Hubs
{
    [Area("Management")]
    //[Authorize(Roles="Manager")]
    public class NoteHub : Hub
    {
        private readonly INoteService noteService;

        public NoteHub(INoteService noteService)
        {
            this.noteService = noteService ?? throw new ArgumentNullException(nameof(noteService));
        }

        public async Task Send(CreateNoteViewModel noteObject)
        {
            await this.noteService.CreateNoteAsync(noteObject);
            var messageJsonString = JsonConvert.SerializeObject(noteObject);
            await this.Clients.All.SendAsync("NewMessage", noteObject);
        }
    }

    public class Message
    {
        public string User { get; set; }

        public string Text { get; set; }
    }
}
