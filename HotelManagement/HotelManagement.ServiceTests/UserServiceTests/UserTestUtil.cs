using HotelManagement.Data;
using HotelManagement.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.ServiceTests.UserServiceTests
{
    public class UserTestUtil
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

            var lb = new LogbookManagers()
            {
                ManagerId = "6536bb9a-3af0-40fe-a878-e5ab8212cd55",
                LogbookId = "fef91cb6-32ed-491d-9e69-81acc2bbe523",
            };

            context.Users.Add(user);

            context.LogbookManagers.Add(lb);

            context.SaveChanges();

            return context;
        }
    }
}
