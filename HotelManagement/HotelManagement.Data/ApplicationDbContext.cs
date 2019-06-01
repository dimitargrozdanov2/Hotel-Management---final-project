using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Data.Configurations;
using HotelManagement.DataModels;
using HotelManagement.DataModels.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HotelManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Feedback> Feedback { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Logbook> Logbooks { get; set; }

        public DbSet<Business> Businesses { get; set; }

        public DbSet<LogbookManagers> LogbookManagers { get; set; }

        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            CheckIsDeleted(builder);

            this.LoadJsonFilesInDatabase(builder);

            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new LogBookManagersConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new FeedbackConfiguration());
            builder.ApplyConfiguration(new ImageConfiguration());
            builder.ApplyConfiguration(new LogbookConfiguration());
            builder.ApplyConfiguration(new LogBookManagersConfiguration());
            builder.ApplyConfiguration(new NoteConfiguration());
            builder.ApplyConfiguration(new BusinessConfiguration());

        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletionRules();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletionRules();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyDeletionRules()
        {
            var entitiesForDeletion = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Deleted && e.Entity is IDeletable);

            foreach (var entry in entitiesForDeletion)
            {
                var entity = (IDeletable)entry.Entity;
                //entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }

        private void ApplyAuditInfoRules()
        {
            var newlyCreatedEntities = this.ChangeTracker.Entries()
                .Where(e => e.Entity is IModifiable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

            foreach (var entry in newlyCreatedEntities)
            {
                var entity = (IModifiable)entry.Entity;

                if (entry.State == EntityState.Added && entity.CreatedOn == null)
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void LoadJsonFilesInDatabase(ModelBuilder modelBuilder)
        {
            var feedbackPath = @"..\HotelManagement.Data\JsonFiles\feedback.json";
            var categoriesPath = @"..\HotelManagement.Data\JsonFiles\categories.json";
            var notesPath = @"..\HotelManagement.Data\JsonFiles\notes.json";
            var logbooksPath = @"..\HotelManagement.Data\JsonFiles\logbooks.json";
            var businessesPath = @"..\HotelManagement.Data\JsonFiles\businesses.json";
            var imagesPath = @"..\HotelManagement.Data\JsonFiles\images.json";

            var logbookManagersPath = @"..\HotelManagement.Data\JsonFiles\logbookManagers.json";


            var usersPath = @"..\HotelManagement.Data\JsonFiles\users.json";
            var rolesPath = @"..\HotelManagement.Data\JsonFiles\roles.json";
            var userRolesPath = @"..\HotelManagement.Data\JsonFiles\userRoles.json";

            var isPathFound = File.Exists(feedbackPath) && File.Exists(categoriesPath) && File.Exists(notesPath)
                                && File.Exists(logbooksPath) && File.Exists(logbookManagersPath) && File.Exists(businessesPath)
                                && File.Exists(imagesPath);
            if (isPathFound)
            {
                var feedback = JsonConvert.DeserializeObject<Feedback[]>(File.ReadAllText(feedbackPath));
                var categories = JsonConvert.DeserializeObject<Category[]>(File.ReadAllText(categoriesPath));
                var notes = JsonConvert.DeserializeObject<Note[]>(File.ReadAllText(notesPath));
                var logbooks = JsonConvert.DeserializeObject<Logbook[]>(File.ReadAllText(logbooksPath));
                var businesses = JsonConvert.DeserializeObject<Business[]>(File.ReadAllText(businessesPath));
                var images = JsonConvert.DeserializeObject<Image[]>(File.ReadAllText(imagesPath));

                var logbookManagers = JsonConvert.DeserializeObject<LogbookManagers[]>(File.ReadAllText(logbookManagersPath));

                var users = JsonConvert.DeserializeObject<User[]>(File.ReadAllText(usersPath));
                var roles = JsonConvert.DeserializeObject<IdentityRole[]>(File.ReadAllText(rolesPath));
                var userRoles = JsonConvert.DeserializeObject<IdentityUserRole<string>[]>(File.ReadAllText(userRolesPath));

                modelBuilder.Entity<Feedback>().HasData(feedback);
                modelBuilder.Entity<Category>().HasData(categories);
                modelBuilder.Entity<Note>().HasData(notes);
                modelBuilder.Entity<Logbook>().HasData(logbooks);
                modelBuilder.Entity<Business>().HasData(businesses);
                modelBuilder.Entity<Image>().HasData(images);

                modelBuilder.Entity<LogbookManagers>().HasData(logbookManagers);


                modelBuilder.Entity<User>().HasData(users);
                modelBuilder.Entity<IdentityRole>().HasData(roles);
                modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);
            }
        }

        private static void CheckIsDeleted(ModelBuilder builder)
        {
            builder.Entity<Note>().HasQueryFilter(b => EF.Property<bool>(b, "IsDeleted") == false);
            builder.Entity<Logbook>().HasQueryFilter(b => EF.Property<bool>(b, "IsDeleted") == false);
            builder.Entity<Feedback>().HasQueryFilter(b => EF.Property<bool>(b, "IsDeleted") == false);
            builder.Entity<Image>().HasQueryFilter(b => EF.Property<bool>(b, "IsDeleted") == false);
            builder.Entity<Business>().HasQueryFilter(b => EF.Property<bool>(b, "IsDeleted") == false);
            builder.Entity<Category>().HasQueryFilter(b => EF.Property<bool>(b, "IsDeleted") == false);
            //builder.Entity<BaseEntity>().HasQueryFilter(b => EF.Property<bool>(b, "IsDeleted") == false);
        }
    }
}
