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
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.ServiceTests.FeedbackServiceTests
{
    [TestClass]
    public class AddComment_Should
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
        public async Task Add_FeedbackToBusiness()
        {
            var databaseName = nameof(Add_FeedbackToBusiness);

            var options = FeedbackTestUtils.GetOptions(databaseName);

            FeedbackTestUtils.FillContextWithBusinesses(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            var model = new AddFeedbackViewModel();
            model.BusinessId = "eaf45030-572b-4af1-add0-bf3b1f979168";
            model.AuthorName = "Ivan";
            model.Comment = "Nice place!";
            model.Email = "ivan@ivan.com";

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new FeedbackService(actAndAssertContext, mappingProviderMock.Object);

                var business = await actAndAssertContext.Businesses.FirstOrDefaultAsync(m => m.Id == model.BusinessId);

                await sut.AddComment(model);

                Assert.IsTrue(business.Feedback.Count == 1);
            }
        }

        [TestMethod]
        public async Task Call_MappingFunction_Once()
        {
            var databaseName = nameof(Call_MappingFunction_Once);

            var options = FeedbackTestUtils.GetOptions(databaseName);

            FeedbackTestUtils.FillContextWithBusinesses(options);

            Feedback feedback = null;

            var mappingProviderMock = new Mock<IMappingProvider>();
            mappingProviderMock
                .Setup(x => x.MapTo<FeedbackViewModel>(It.IsAny<Feedback>()))
                .Callback<object>(inputargs => feedback = inputargs as Feedback);

            var model = new AddFeedbackViewModel();
            model.BusinessId = "eaf45030-572b-4af1-add0-bf3b1f979168";
            model.AuthorName = "Ivan";
            model.Comment = "Nice place!";
            model.Email = "ivan@ivan.com";

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new FeedbackService(actAndAssertContext, mappingProviderMock.Object);

                var business = await actAndAssertContext.Businesses.FirstOrDefaultAsync(m => m.Id == model.BusinessId);

                var feedbackReturn = await sut.AddComment(model);

                mappingProviderMock.Verify(m => m.MapTo<FeedbackViewModel>(feedback), Times.Once);
            }
        }
    }
}
