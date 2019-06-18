using HotelManagement.Data;
using HotelManagement.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HotelManagement.ServiceTests.CategoryServiceTests
{
    public class CategoryTestUtil
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

        public static ApplicationDbContext FillContextWithCategories(DbContextOptions<ApplicationDbContext> options)
        {
            var context = new ApplicationDbContext(options);

            var logbook = new Logbook()
            {
                Name = "Swimming Pool",
                Description = "A renovated swimming pool for kids and adults!",
                Id = "03f02eb0-073d-4c48-a641-5b105831cac3"
            };
            var category = new Category()
            {
                Name = "Maintenance",
                LogbookId = "03f02eb0-073d-4c48-a641-5b105831cac3"
            };
            context.Logbooks.Add(logbook);

            context.Categories.Add(category);

            logbook.Categories.Add(category);

            context.SaveChanges();

            return context;
        }
    }
}