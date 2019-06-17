using HotelManagement.Services.Contracts;
using HotelManagement.ViewModels;
using HotelManagement.Web.Areas.Management.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace HotelManagement.ControllerTests.ManagementControllerTests
{
    [TestClass]
    public class CreateNote_Should
    {
        [TestMethod]
        public async Task ReturnCorrectViewModel_OnGet()
        {
            // Arrange
            string logbookName = "logbookName";

            var userServiceMock = new Mock<IUserService>();
            var noteServiceMock = new Mock<INoteService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock
            .Setup(g => g.GetAllCategoryNamesAsync(logbookName))
                .ReturnsAsync(new List<string>());


            var sut = new ManagementController(userServiceMock.Object, noteServiceMock.Object, categoryServiceMock.Object);

            // Act
            var result = await sut.CreateNote(logbookName) as JsonResult;

            // Assert   
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public async Task CallCategoryServiceOnce_OnPost()
        {
            // Arrange
            string logbookName = "logbookName";

            var userServiceMock = new Mock<IUserService>();
            var noteServiceMock = new Mock<INoteService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock
            .Setup(g => g.GetAllCategoryNamesAsync(logbookName))
                .ReturnsAsync(new List<string>());


            var sut = new ManagementController(userServiceMock.Object, noteServiceMock.Object, categoryServiceMock.Object);

            // Act
            var result = await sut.CreateNote(logbookName) as JsonResult;

            // Assert   
            categoryServiceMock.Verify(g => g.GetAllCategoryNamesAsync(logbookName), Times.Once);
        }
    }
}
