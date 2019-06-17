using HotelManagement.Services.Contracts;
using HotelManagement.Services.Wrappers.Contracts;
using HotelManagement.ViewModels;
using HotelManagement.Web.Controllers;
using HotelManagement.Web.Models.HomeViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.ControllerTests.HomeControllerTests
{
    [TestClass]
    public class Index_Should
    {
        [TestMethod]
        public async Task CallBusinessServiceOnce()
        {
            // Arrange
            var businessServiceMock = new Mock<IBusinessService>();
            var userServiceMock = new Mock<IUserService>();
            var userManagerWrapperMock = new Mock<IUserManagerWrapper>();

            string getBy = "date";

            businessServiceMock
                .Setup(g => g.GetBusinesses(getBy, true))
                .ReturnsAsync(new List<BusinessViewModel>());

            var sut = new HomeController(businessServiceMock.Object, userServiceMock.Object, userManagerWrapperMock.Object);

            // Act
            await sut.Index();

            // Assert
            businessServiceMock.Verify(g => g.GetBusinesses(getBy, true), Times.Once);
        }

        [TestMethod]
        public async Task Return_Correct_ViewModel()
        {
            // Arrange
            var businessServiceMock = new Mock<IBusinessService>();
            var userServiceMock = new Mock<IUserService>();
            var userManagerWrapperMock = new Mock<IUserManagerWrapper>();

            string getBy = "date";

            businessServiceMock
                .Setup(g => g.GetBusinesses(getBy, true))
                .ReturnsAsync(new List<BusinessViewModel>());

            var sut = new HomeController(businessServiceMock.Object, userServiceMock.Object, userManagerWrapperMock.Object);

            // Act
            var result = await sut.Index() as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(HomeIndexViewModel));
        }
    }
}
