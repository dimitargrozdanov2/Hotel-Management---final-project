using HotelManagement.Services.Contracts;
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
    public class Search_Should
    {
        [TestMethod]
        public void ReturnCorrectViewModel_OnGet()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var noteServiceMock = new Mock<INoteService>();
            var categoryServiceMock = new Mock<ICategoryService>();

            string noteName = "noNote";

            var sut = new ManagementController(userServiceMock.Object, noteServiceMock.Object, categoryServiceMock.Object);

            // Act
            var result = sut.Search(noteName) as ViewResult;

            // Assert   
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
