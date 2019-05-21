using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.Services.Contracts;
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
         
        public async Task<ICollection<Note>> GetNotes()
        {
            var notes = await this.dbContext.Notes
                .Include(l => l.Logbook)
                    .ThenInclude(x => x.LogbookManagers)
                .Include(c => c.Category)
                .Include(u => u.User).ToListAsync();

            return notes;
        }
    }
}
