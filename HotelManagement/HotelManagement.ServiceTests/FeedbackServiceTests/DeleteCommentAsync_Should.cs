using HotelManagement.Data;
using HotelManagement.Infrastructure;
using HotelManagement.Services;
using HotelManagement.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace HotelManagement.ServiceTests.FeedbackServiceTests
{
    [TestClass]
    public class DeleteCommentAsync_Should
    {
        [TestMethod]
        public async Task Throw_When_FeedbackDoesNotExist()
        {
            var databaseName = nameof(Throw_When_FeedbackDoesNotExist);

            var options = FeedbackTestUtils.GetOptions(databaseName);

            var mappingProviderMock = new Mock<IMappingProvider>();

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new FeedbackService(actAndAssertContext, mappingProviderMock.Object);

                await Assert.ThrowsExceptionAsync<EntityInvalidException>(
                    async () => await sut.DeleteCommentAsync("InvalidID"));
            }
        }

        [TestMethod]
        public async Task Remove_Feedback_FromDbContext()
        {
            var databaseName = nameof(Throw_When_FeedbackDoesNotExist);

            var options = FeedbackTestUtils.GetOptions(databaseName);
            FeedbackTestUtils.FillContextWithBusinesses(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new FeedbackService(actAndAssertContext, mappingProviderMock.Object);

                var feedbackId = "a4a0911d-7787-4e2e-bff0-1c18bc71eb16";
                await sut.DeleteCommentAsync(feedbackId);

                var feedback = await actAndAssertContext.Feedback
                .FirstOrDefaultAsync(l => l.Id == feedbackId);

                Assert.IsTrue(feedback == null);
            }
        }
    }
}