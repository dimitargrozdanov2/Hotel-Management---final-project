using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.Infrastructure;
using HotelManagement.Services;
using HotelManagement.ViewModels;
using HotelManagement.ViewModels.PublicArea;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.ServiceTests.FeedbackServiceTests
{
    [TestClass]
    public class AddReply_Should
    {
        [TestMethod]
        public async Task Throw_When_BusinessDoesNotExist()
        {
            var databaseName = nameof(Throw_When_BusinessDoesNotExist);

            var options = FeedbackTestUtils.GetOptions(databaseName);

            var mappingProviderMock = new Mock<IMappingProvider>();

            var parameterModel = new AddFeedbackViewModel();

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new FeedbackService(actAndAssertContext, mappingProviderMock.Object);

                await Assert.ThrowsExceptionAsync<ArgumentException>(
                    async () => await sut.AddComment(parameterModel));
            }
        }

        [TestMethod]
        public async Task Add_ReplyToMainFeedback()
        {
            var databaseName = nameof(Add_ReplyToMainFeedback);

            var options = FeedbackTestUtils.GetOptions(databaseName);

            FeedbackTestUtils.FillContextWithBusinesses(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            var model = new AddFeedbackViewModel();
            model.BusinessId = "2fce4ada-9ca4-450c-8916-c92f4ffa2dd4";
            model.FeedbackParentId = "a4a0911d-7787-4e2e-bff0-1c18bc71eb16";
            model.AuthorName = "Marge";
            model.Comment = "Nice restaurant!";
            model.Email = "marge@marge.com";

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new FeedbackService(actAndAssertContext, mappingProviderMock.Object);

                var business = await actAndAssertContext.Businesses
                    .Include(x => x.Feedback)
                    .FirstOrDefaultAsync(m => m.Id == model.BusinessId);

                var feedback = await sut.AddReply(model);

                Assert.IsTrue(business.Feedback.FirstOrDefault(x => x.Id == model.FeedbackParentId)
                    .Replies.Count == 1);
            }
        }

        [TestMethod]
        public async Task Call_MappingFunction_Once()
        {
            var databaseName = nameof(Add_ReplyToMainFeedback);

            var options = FeedbackTestUtils.GetOptions(databaseName);

            FeedbackTestUtils.FillContextWithBusinesses(options);

            Feedback feedback = null;

            var mappingProviderMock = new Mock<IMappingProvider>();
            mappingProviderMock
                .Setup(x => x.MapTo<FeedbackViewModel>(It.IsAny<Feedback>()))
                .Callback<object>(inputargs => feedback = inputargs as Feedback);

            var model = new AddFeedbackViewModel();
            model.BusinessId = "2fce4ada-9ca4-450c-8916-c92f4ffa2dd4";
            model.FeedbackParentId = "a4a0911d-7787-4e2e-bff0-1c18bc71eb16";
            model.AuthorName = "Marge";
            model.Comment = "Nice restaurant!";
            model.Email = "marge@marge.com";

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new FeedbackService(actAndAssertContext, mappingProviderMock.Object);

                var business = await actAndAssertContext.Businesses
                    .Include(x => x.Feedback)
                    .FirstOrDefaultAsync(m => m.Id == model.BusinessId);

                await sut.AddReply(model);

                mappingProviderMock.Verify(m => m.MapTo<FeedbackViewModel>(feedback), Times.Once);
            }
        }

    }
}
