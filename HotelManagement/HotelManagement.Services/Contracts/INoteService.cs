﻿using HotelManagement.DataModels;
using HotelManagement.ViewModels;
using HotelManagement.ViewModels.Management;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services.Contracts
{
    public interface INoteService
    {
        Task<ICollection<Note>> GetNotes();

        Task<NoteViewModel> CreateNoteAsync(CreateNoteViewModel model);

        Task<string> DeleteNoteAsync(string id);


    }
}
