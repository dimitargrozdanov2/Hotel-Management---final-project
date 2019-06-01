using HotelManagement.DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services.Contracts
{
    public interface INoteService
    {
        Task<ICollection<Note>> GetNotes();
    }
}
