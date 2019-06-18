using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.DataModels.Enums;
using HotelManagement.Infrastructure;
using HotelManagement.Services.Contracts;
using HotelManagement.ViewModels;
using HotelManagement.ViewModels.Management;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var logbook = await this.dbContext.Logbooks
                .Include(m => m.LogbookManagers)
                    .ThenInclude(m => m.Manager)
                .FirstOrDefaultAsync(l => l.Name == model.Logbook && l.LogbookManagers.Any(m => m.Manager.Email == model.Email));

            if (logbook == null)
            {
                throw new ArgumentException($"{model.Logbook} logbook has not been found!");
            }

            var category = await this.dbContext.Categories.FirstOrDefaultAsync(c => c.Name == model.Category);
            if (category == null)
            {
                throw new ArgumentException($"{model.Logbook} category has not been found!");
            }

            var user = await this.dbContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

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

        public async Task<string> DeleteNoteAsync(string id)
        {
            var note = await this.dbContext.Notes.FirstOrDefaultAsync(l => l.Id == id);

            if (note == null)
            {
                throw new ArgumentException($"Note with name `{id}` has not been found!");
            }

            this.dbContext.Notes.Remove(note);
            await this.dbContext.SaveChangesAsync();

            return id;
        }

        public async Task<ICollection<NoteViewModel>> SearchNotesAsync(string data, string userIdentity, string searchByValue)
        {
            ICollection<Note> notes;

            if (searchByValue == "Text")
            {
                notes = await this.dbContext.Notes
                .Include(l => l.Logbook)
                    .ThenInclude(x => x.LogbookManagers)
                .Include(c => c.Category)
                .Include(u => u.User)
                .Where(n => n.Text.Contains(data) && n.Logbook.LogbookManagers.Any(x => x.Manager.Email == userIdentity))
                .OrderBy(d => d.CreatedOn)
                .Take(10)
                .ToListAsync();
            }
            else if (searchByValue == "Category")
            {
                notes = await this.dbContext.Notes
                .Include(l => l.Logbook)
                .Include(c => c.Category)
                .Include(u => u.User)
                .Where(n => n.Category.Name == data && n.Logbook.LogbookManagers.Any(x => x.Manager.Email == userIdentity))
                .OrderBy(d => d.CreatedOn)
                .ToListAsync();
            }
            else
            {
                notes = await this.dbContext.Notes
                .Include(l => l.Logbook)
                .Include(c => c.Category)
                .Include(u => u.User)
                .Where(n => n.CreatedOn.Value.ToShortDateString().ToString() == data && n.Logbook.LogbookManagers.Any(x => x.Manager.Email == userIdentity))
                .OrderBy(d => d.CreatedOn)
                .ToListAsync();
            }

            var mappedNotes = this.mappingProvider.MapTo<ICollection<NoteViewModel>>(notes);
            return mappedNotes;
        }
    }
}