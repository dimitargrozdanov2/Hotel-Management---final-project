using HotelManagement.Data;
using HotelManagement.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HotelManagement.ServiceTests.NoteServiceTests
{
    public class NoteTestUtils
    {
        public static DbContextOptions<ApplicationDbContext> GetOptions(string databaseName)
        {
            var serviceCollection = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName)
                .UseInternalServiceProvider(serviceCollection)
                .Options;
        }

        public static ApplicationDbContext FillContextWithUserData(DbContextOptions<ApplicationDbContext> options)
        {
            var context = new ApplicationDbContext(options);

            var ChickenB = new Business()
            {
                Id = "d26bd969-8209-4e5e-a72e-0f8dc4c7dffa",
                Name = "ChickenB",
                Location = "Sofia",
                Description = "Basic Chicken business!"
            };

            context.Businesses.Add(ChickenB);

            var logbook = new Logbook()
            {
                Name = "Restaurant",
                Description = "Good looking and delicious restaurant!",
                Id = "fef91cb6-32ed-491d-9e69-81acc2bbe523",
                BusinessId = "d26bd969-8209-4e5e-a72e-0f8dc4c7dffa",
            };

            context.Logbooks.Add(logbook);

            var user = new User()
            {
                Id = "6536bb9a-3af0-40fe-a878-e5ab8212cd55",
                UserName = "admin@admin.admin",
                Email = "admin@admin.admin",
            };

            var category = new Category()
            {
                Name = "TODO",
                Id = "b3905927-12d6-4227-b6a2-2c0677ce6cae",
                LogbookId = "fef91cb6-32ed-491d-9e69-81acc2bbe523"
            };

            var secondCategory = new Category()
            {
                Name = "Fixes",
                Id = "29eab335-a7d3-42b2-be09-6214d387a4da",
                LogbookId = "fef91cb6-32ed-491d-9e69-81acc2bbe523"
            };

            var lb = new LogbookManagers()
            {
                ManagerId = "6536bb9a-3af0-40fe-a878-e5ab8212cd55",
                LogbookId = "fef91cb6-32ed-491d-9e69-81acc2bbe523",
            };

            context.Users.Add(user);

            context.Categories.Add(category);
            context.Categories.Add(secondCategory);

            context.LogbookManagers.Add(lb);

            context.SaveChanges();

            return context;
        }
    }
}