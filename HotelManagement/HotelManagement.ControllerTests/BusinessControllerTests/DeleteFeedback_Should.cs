using HotelManagement.Services.Contracts;
using HotelManagement.Web.Areas.Business.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace HotelManagement.ControllerTests.BusinessControllerTests
{
    [TestClass]
    public class DeleteFeedback_Should
    {
        [TestMethod]
        public async Task Return_BadRequest_IfModelInvalid_OnPost()
        {
            // Arrange
            var businessService = new Mock<IBusinessService>();
            var feedbackService = new Mock<IFeedbackService>();

            var sut = new BusinessController(businessService.Object, feedbackService.Object);
            sut.ModelState.AddModelError("error", "error");
            // Act
            var result = await sut.DeleteFeedback("Id");

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task Call_FeedbackService_With_Correct_Params()
        {
            // Arrange
            var id = "randomId";

            var businessService = new Mock<IBusinessService>();
            var feedbackService = new Mock<IFeedbackService>();
            feedbackService.Setup(f => f.DeleteCommentAsync(id)).ReturnsAsync(id);

            var sut = new BusinessController(businessService.Object, feedbackService.Object);
            // Act
            var result = await sut.DeleteFeedback(id);

            // Assert
            feedbackService.Verify(x => x.DeleteCommentAsync(id), Times.Once);
        }

        [TestMethod]
        public async Task Return_Correct_StatusCode_200()
        {
            // Arrange
            var id = "randomId";

            var businessService = new Mock<IBusinessService>();
            var feedbackService = new Mock<IFeedbackService>();
            feedbackService.Setup(f => f.DeleteCommentAsync("Hi")).ReturnsAsync(id);

            var sut = new BusinessController(businessService.Object, feedbackService.Object);
            // Act
            var result = await sut.DeleteFeedback(id) as StatusCodeResult;

            // Assert
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public async Task Return_StatusCode_When_Exception_IsThrown()
        {
            // Arrange
            var id = "randomId";

            var businessService = new Mock<IBusinessService>();
            var feedbackService = new Mock<IFeedbackService>();
            feedbackService.Setup(f => f.DeleteCommentAsync(id)).Throws<Exception>();

            var sut = new BusinessController(businessService.Object, feedbackService.Object);
            // Act
            var result = await sut.DeleteFeedback(id) as ObjectResult;

            Assert.AreEqual(500, result.StatusCode);
        }
    }
}