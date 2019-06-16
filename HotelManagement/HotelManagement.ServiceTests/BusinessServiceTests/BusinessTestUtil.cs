using HotelManagement.Data;
using HotelManagement.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.ServiceTests.BusinessServiceTests
{
    public class BusinessTestUtil
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
        public static ApplicationDbContext FillContextWithBusinesses(DbContextOptions<ApplicationDbContext> options)
        {
            var context = new ApplicationDbContext(options);

            var pastaBusiness = new Business()
            {
                Id = "4f3e3358-055f-49f6-a48e-5e0c9d9fe6c8",
                Name = "GROS",
                Location = "Montana",
                Description = "Gros is a pasta manufacturer."
            };

            var logbook = new Logbook()
            {
                Name = "Manufacturing",
                Description = "One of the core procedures in the factory is manufacturing the different kinds of pasta",
                Id = "7d7292ab-30d8-4c30-b972-568fcd1000eb",
                BusinessId = "4f3e3358-055f-49f6-a48e-5e0c9d9fe6c8"
            };

            var hotelBusiness = new Business()
            {
                Id = "6bc4021b-1c35-48b7-8d3b-a853bfc5e050",
                Name = "Hilton",
                Location = "London",
                Description = "Hilton Hotels & Resorts is a global brand of full service hotels and resorts"
            };

            var pastaLogo = new Image()
            {
                Name = "GROS_logo.jpg",
                BusinessId = "4f3e3358-055f-49f6-a48e-5e0c9d9fe6c8"
            };

            context.Businesses.Add(pastaBusiness);

            context.Logbooks.Add(logbook);

            context.Images.Add(pastaLogo);

            pastaBusiness.BusinessUnits.Add(logbook);

            pastaBusiness.Images.Add(pastaLogo);

            context.Businesses.Add(hotelBusiness);

            context.SaveChanges();

            return context;

        }
    }
}
