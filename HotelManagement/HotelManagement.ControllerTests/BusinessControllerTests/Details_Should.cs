using HotelManagement.Services.Contracts;
using HotelManagement.ViewModels;
using HotelManagement.Web.Areas.Business.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.ControllerTests.BusinessControllerTests
{
    [TestClass]
    public class Details_Should
    {
        [TestMethod]
        public async Task CallBusinessServiceOnce_OnGet()
        {
            // Arrange
            var businessService = new Mock<IBusinessService>();
            var feedbackService = new Mock<IFeedbackService>();

            string businessName = "BusinessName";

            businessService
                .Setup(g => g.GetBusinessByNameAsync(businessName))
                .ReturnsAsync(new BusinessViewModel());

            var sut = new BusinessController(businessService.Object, feedbackService.Object);

            // Act
            await sut.Details(businessName);

            // Assert
            businessService.Verify(g => g.GetBusinessByNameAsync(businessName), Times.Once);
        }

        [TestMethod]
        public async Task ReturnCorrectViewModel_OnGet()
        {
            // Arrange
            var businessService = new Mock<IBusinessService>();
            var feedbackService = new Mock<IFeedbackService>();

            string businessName = "BusinessName";

            businessService
                .Setup(g => g.GetBusinessByNameAsync(businessName))
                .ReturnsAsync(new BusinessViewModel());

            var sut = new BusinessController(businessService.Object, feedbackService.Object);

            // Act
           var result = await sut.Details(businessName) as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(BusinessViewModel));
        }
    }
}
