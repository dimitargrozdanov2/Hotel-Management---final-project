using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.Services.Contracts;
using HotelManagement.ViewModels;
using HotelManagement.ViewModels.Management;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public class NoteService : INoteService
    {
        private readonly ApplicationDbContext dbContext;

        public NoteService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task<NoteViewModel> CreateNoteAsync(CreateNoteViewModel model)
        {
            var logbook = this.dbContext.Logbooks.FirstOrDefaultAsync(l => l.Id == model.LogbookId);

            // add functionality to create note; TODO

            return null;
        }

        // Having 2 thenInclude on the same property written twice is okay
        //  ..because ef will NOT generate redundant joins
        public async Task<ICollection<Note>> GetNotes()
        {
            var notes = await this.dbContext.Notes
                .Include(l => l.Logbook)
                    .ThenInclude(x => x.LogbookManagers)
                .Include(x => x.Logbook)
                    .ThenInclude(s => s.Business)
                .Include(c => c.Category)
                .Include(u => u.User).ToListAsync();

            return notes;
        }
    }
}
