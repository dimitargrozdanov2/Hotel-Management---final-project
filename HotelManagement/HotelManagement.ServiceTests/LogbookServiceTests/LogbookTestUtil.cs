﻿using HotelManagement.Data;
using HotelManagement.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.ServiceTests.LogbookServiceTests
{
    public class LogbookTestUtil
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

        public static ApplicationDbContext FillContextWithLogbooks(DbContextOptions<ApplicationDbContext> options)
        {
            var context = new ApplicationDbContext(options);

            var pastaBusiness = new Business()
            {
                Id = "4f3e3358-055f-49f6-a48e-5e0c9d9fe6c8",
                Name = "GROS",
                Location = "Montana",
                Description = "Gros is a pasta manufacturer."
            };

            var manufacturingLogbook = new Logbook()
            {
                Name = "Manufacturing",
                Description = "One of the core procedures in the factory is manufacturing the different kinds of pasta",
                Id = "7d7292ab-30d8-4c30-b972-568fcd1000eb",
                BusinessId = "4f3e3358-055f-49f6-a48e-5e0c9d9fe6c8"
            };

            var hotelApartment = new Business()
            {
                Id = "6bc4021b-1c35-48b7-8d3b-a853bfc5e050",
                Name = "Papantonia",
                Location = "Paralimni",
                Description = "A 9-minute walk from the closest beach, this relaxed apartment hotel is 1 km from shops and restaurants in Pernera, 4 km from Protaras"
            };

            var swimPoolLogbook = new Logbook()
            {
                Name = "Swimming Pool",
                Description = "A renovated swimming pool for kids and adults!",
                Id = "03f02eb0-073d-4c48-a641-5b105831cac3",
                BusinessId = "6bc4021b-1c35-48b7-8d3b-a853bfc5e050"
            };
            context.Businesses.Add(pastaBusiness);

            context.Logbooks.Add(manufacturingLogbook);

            pastaBusiness.BusinessUnits.Add(manufacturingLogbook);

            context.Businesses.Add(hotelApartment);

            context.Logbooks.Add(swimPoolLogbook);

            hotelApartment.BusinessUnits.Add(swimPoolLogbook);

            context.SaveChanges();

            return context;
        }
    }
}