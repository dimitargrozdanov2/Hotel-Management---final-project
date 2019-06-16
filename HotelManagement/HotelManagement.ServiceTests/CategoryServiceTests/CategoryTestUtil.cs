using HotelManagement.Data;
using HotelManagement.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

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

            context.Logbooks.Add(new Logbook()
            {
                Name = "Disco",
                Description = "A new room for clubbing has opened",
                Id = "a0ab4621-a92c-4735-bcab-3e35eabae03d"
            });

            context.Logbooks.Add(new Logbook()
            {
                Name = "Swimming Pool",
                Description = "A renovated swimming pool for kids and adults!",
                Id = "03f02eb0-073d-4c48-a641-5b105831cac3"
            });

            context.Categories.Add(new Category() { Name = "Maintenance", LogbookId = "03f02eb0-073d-4c48-a641-5b105831cac3" });

            context.Categories.Add(new Category() { Name = "Entertainment", LogbookId = "a0ab4621-a92c-4735-bcab-3e35eabae03d" });

            context.SaveChanges();

            return context;
        }
    }
}
