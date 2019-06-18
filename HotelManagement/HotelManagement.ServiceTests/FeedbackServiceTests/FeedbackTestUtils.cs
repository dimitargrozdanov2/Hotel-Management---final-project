using HotelManagement.Data;
using HotelManagement.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HotelManagement.ServiceTests.FeedbackServiceTests
{
    public class FeedbackTestUtils
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

            var ChickenB = new Business()
            {
                Id = "eaf45030-572b-4af1-add0-bf3b1f979168",
                Name = "ChickenB",
                Location = "Sofia",
                Description = "Basic Chicken business!"
            };

            var BurgerKing = new Business()
            {
                Id = "2fce4ada-9ca4-450c-8916-c92f4ffa2dd4",
                Name = "BurgerKing",
                Location = "Sofia",
                Description = "Yet another BurgerKing business!"
            };

            var feedback = new Feedback()
            {
                BusinessId = "2fce4ada-9ca4-450c-8916-c92f4ffa2dd4",
                Id = "a4a0911d-7787-4e2e-bff0-1c18bc71eb16",
                Name = "George",
                Comment = "Awesome place!",
                Email = "george@george.com"
            };

            //var feedbackReply = new Feedback()
            //{
            //    BusinessId = "2fce4ada-9ca4-450c-8916-c92f4ffa2dd4",
            //    Id = "2e738a2f-c365-4a02-8a0d-4563b96d36e4",
            //    FeedbackParentId = "a4a0911d-7787-4e2e-bff0-1c18bc71eb16",
            //    Name = "Marge",
            //    Comment = "Nice restaurant!",
            //    Email = "marge@marge.com"
            //};

            context.Businesses.Add(ChickenB);

            context.Businesses.Add(BurgerKing);

            context.Feedback.Add(feedback);
            //context.Feedback.Add(feedbackReply);

            context.SaveChanges();

            return context;
        }
    }
}