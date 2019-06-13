using HotelManagement.DataModels;
using HotelManagement.ViewModels;
using HotelManagement.ViewModels.Management;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.Services.Contracts
{
    public interface INoteService
    {
        Task<ICollection<Note>> GetNotes();

        Task<NoteViewModel> CreateNoteAsync(CreateNoteViewModel model);

        Task<string> DeleteNoteAsync(string id);

        ICollection<NoteViewModel> SearchByTextAsync(string data, string userIdentity);
    }
}
