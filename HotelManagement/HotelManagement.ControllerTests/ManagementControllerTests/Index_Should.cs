using HotelManagement.Services.Contracts;
using HotelManagement.ViewModels;
using HotelManagement.ViewModels.Management;
using HotelManagement.Web.Areas.Management.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.ControllerTests.ManagementControllerTests
{
    [TestClass]
    public class Index_Should
    {
        [TestMethod]
        public async Task CalluserServiceOnce_OnPost()
        {
            // Arrange
            string email = "admin@admin.admin";
            string specifiedLogbook = "LogbookTwo";

            var model = new ManagementIndexViewModel();

            var collectionOfLogbooks = new List<LogbookViewModel>
            {
                new LogbookViewModel()
                {
                    Name = "LogbookOne"
                },
                new LogbookViewModel()
                {
                    Name = "LogbookTwo"
                }
            };

            var userServiceMock = new Mock<IUserService>();
            var noteServiceMock = new Mock<INoteService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            userServiceMock
            .Setup(g => g.GetUserLogbooksAsync(email))
                .ReturnsAsync(collectionOfLogbooks);

            var sut = new ManagementController(userServiceMock.Object, noteServiceMock.Object, categoryServiceMock.Object);

            // Act
            var result = await sut.Index(email, specifiedLogbook) as ViewResult;

            userServiceMock.Verify(u => u.GetUserLogbooksAsync(email), Times.Once);
        }

        [TestMethod]
        public async Task ReturnCorrectViewModel_OnPost()
        {
            // Arrange
            string email = "admin@admin.admin";
            string specifiedLogbook = "LogbookTwo";

            var model = new ManagementIndexViewModel();

            var collectionOfLogbooks = new List<LogbookViewModel>
            {
                new LogbookViewModel()
                {
                    Name = "LogbookOne"
                },
                new LogbookViewModel()
                {
                    Name = "LogbookTwo"
                }
            };

            var userServiceMock = new Mock<IUserService>();
            var noteServiceMock = new Mock<INoteService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            userServiceMock
            .Setup(g => g.GetUserLogbooksAsync(email))
                .ReturnsAsync(collectionOfLogbooks);

            var sut = new ManagementController(userServiceMock.Object, noteServiceMock.Object, categoryServiceMock.Object);

            // Act
            var result = await sut.Index(email, specifiedLogbook) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(ManagementIndexViewModel));
        }

        [TestMethod]
        public async Task Assign_SpecifiedLogbook_When_IsNotNull_Correctly()
        {
            // Arrange
            string email = "admin@admin.admin";
            string specifiedLogbook = "LogbookTwo";

            var model = new ManagementIndexViewModel();

            var collectionOfLogbooks = new List<LogbookViewModel>
            {
                new LogbookViewModel()
                {
                    Name = "LogbookOne"
                },
                new LogbookViewModel()
                {
                    Name = "LogbookTwo"
                }
            };

            var userServiceMock = new Mock<IUserService>();
            var noteServiceMock = new Mock<INoteService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            userServiceMock
            .Setup(g => g.GetUserLogbooksAsync(email))
                .ReturnsAsync(collectionOfLogbooks);

            var sut = new ManagementController(userServiceMock.Object, noteServiceMock.Object, categoryServiceMock.Object);

            // Act
            var result = await sut.Index(email, specifiedLogbook) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(ManagementIndexViewModel));

            var castedManagementModel = (ManagementIndexViewModel)result.Model;

            Assert.IsTrue(castedManagementModel.SpecifiedLogbook.Name == specifiedLogbook);
        }

        [TestMethod]
        public async Task Assign_SpecifiedLogbook_WhenIsNull_Correctly()
        {
            // Arrange
            string email = "admin@admin.admin";
            string logbookOne = "LogbookOne";

            var model = new ManagementIndexViewModel();

            var collectionOfLogbooks = new List<LogbookViewModel>
            {
                new LogbookViewModel()
                {
                    Name = "LogbookOne"
                },
                new LogbookViewModel()
                {
                    Name = "LogbookTwo"
                }
            };

            var userServiceMock = new Mock<IUserService>();
            var noteServiceMock = new Mock<INoteService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            userServiceMock
            .Setup(g => g.GetUserLogbooksAsync(email))
                .ReturnsAsync(collectionOfLogbooks);

            var sut = new ManagementController(userServiceMock.Object, noteServiceMock.Object, categoryServiceMock.Object);

            // Act
            var result = await sut.Index(email, null) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(ManagementIndexViewModel));

            var castedManagementModel = (ManagementIndexViewModel)result.Model;

            Assert.IsTrue(castedManagementModel.SpecifiedLogbook.Name == logbookOne);
        }
    }
}