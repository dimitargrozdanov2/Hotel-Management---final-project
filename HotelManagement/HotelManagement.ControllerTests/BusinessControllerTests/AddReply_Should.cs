using HotelManagement.Services.Contracts;
using HotelManagement.ViewModels;
using HotelManagement.ViewModels.PublicArea;
using HotelManagement.Web.Areas.Business.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace HotelManagement.ControllerTests.BusinessControllerTests
{
    [TestClass]
    public class AddReply_Should
    {
        [TestMethod]
        public async Task Return_BadRequest_IfModelInvalid_OnPost()
        {
            // Arrange
            var businessService = new Mock<IBusinessService>();
            var feedbackService = new Mock<IFeedbackService>();

            var model = new AddFeedbackViewModel();

            var sut = new BusinessController(businessService.Object, feedbackService.Object);
            sut.ModelState.AddModelError("error", "error");
            // Act
            var result = await sut.AddReply(model);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task Call_FeedbackService_With_Correct_Params()
        {
            // Arrange
            var model = new AddFeedbackViewModel();
            model.BusinessId = "eaf45030-572b-4af1-add0-bf3b1f979168";
            model.AuthorName = "Ivan";
            model.Comment = "Nice place!";
            model.Email = "ivan@ivan.com";

            var businessService = new Mock<IBusinessService>();
            var feedbackService = new Mock<IFeedbackService>();
            feedbackService.Setup(f => f.AddReply(model)).ReturnsAsync(It.IsAny<FeedbackViewModel>());

            var sut = new BusinessController(businessService.Object, feedbackService.Object);
            // Act
            var result = await sut.Comment(model);

            // Assert
            feedbackService.Verify(x => x.AddComment(model), Times.Once);
        }

        [TestMethod]
        public async Task Return_Correct_PartialView_WhenPassedValidParams()
        {
            // Arrange
            var model = new AddFeedbackViewModel();
            model.BusinessId = "eaf45030-572b-4af1-add0-bf3b1f979168";
            model.AuthorName = "Ivan";
            model.Comment = "Nice place!";
            model.Email = "ivan@ivan.com";

            var feedbackModel = new FeedbackViewModel();

            var businessService = new Mock<IBusinessService>();
            var feedbackService = new Mock<IFeedbackService>();
            feedbackService.Setup(f => f.AddReply(model)).ReturnsAsync(feedbackModel);

            var sut = new BusinessController(businessService.Object, feedbackService.Object);
            // Act
            var result = await sut.AddReply(model) as PartialViewResult;

            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(FeedbackViewModel));
        }

        [TestMethod]
        public async Task Return_StatusCode_When_Exception_IsThrown()
        {
            // Arrange
            var model = new AddFeedbackViewModel();
            model.BusinessId = "eaf45030-572b-4af1-add0-bf3b1f979168";
            model.AuthorName = null;
            model.Comment = "Nice place!";
            model.Email = "ivan@ivan.com";

            var feedbackModel = new FeedbackViewModel();

            var businessService = new Mock<IBusinessService>();
            var feedbackService = new Mock<IFeedbackService>();
            feedbackService.Setup(f => f.AddReply(model)).Throws<Exception>();

            var sut = new BusinessController(businessService.Object, feedbackService.Object);
            // Act
            var result = await sut.AddReply(model) as ObjectResult;

            Assert.AreEqual(500, result.StatusCode);
        }
    }
}
