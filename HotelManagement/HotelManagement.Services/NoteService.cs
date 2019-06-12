using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.DataModels.Enums;
using HotelManagement.Infrastructure;
using HotelManagement.Services.Contracts;
using HotelManagement.Services.Exceptions;
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
        private readonly IMappingProvider mappingProvider;

        public NoteService(ApplicationDbContext dbContext, IMappingProvider mappingProvider)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.mappingProvider = mappingProvider ?? throw new ArgumentNullException(nameof(mappingProvider));
        }

        public async Task<NoteViewModel> CreateNoteAsync(CreateNoteViewModel model)
        {
            var logbook = await this.dbContext.Logbooks.FirstOrDefaultAsync(l => l.Name == model.Logbook);

            if(logbook == null)
            {
                throw new EntityInvalidException($"`{model.Logbook}` logbook has not been found!");
            }

            var category = await this.dbContext.Categories.FirstOrDefaultAsync(c => c.Name == model.Category);
            if (category == null)
            {
                throw new EntityInvalidException($"`{model.Logbook}` category has not been found!");
            }

            var user = await this.dbContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                throw new EntityInvalidException($"`{model.Email}` user has not been found!");
            }

            var note = new Note()
            {
                Text = model.Text,
                Category = category,
                PriorityType = (PriorityType)Enum.Parse(typeof(PriorityType), model.Priority),
                User = user,
                Logbook = logbook
            };

            await this.dbContext.Notes.AddAsync(note);
            await this.dbContext.SaveChangesAsync();
            var mappedNote = this.mappingProvider.MapTo<NoteViewModel>(note);
            return mappedNote;
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
