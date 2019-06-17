using HotelManagement.DataModels;
using HotelManagement.ViewModels;
using HotelManagement.ViewModels.Management;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.Services.Contracts
{
    public interface INoteService
    {
        Task<NoteViewModel> CreateNoteAsync(CreateNoteViewModel model);

        Task<string> DeleteNoteAsync(string id);

        Task<ICollection<NoteViewModel>> SearchNotesAsync(string data, string userIdentity, string searchByValue);
    }
}
