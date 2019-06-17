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
    public class DeleteNote_Should
    {
        [TestMethod]
        public async Task Return_BadRequest_IfModelInvalid_OnPost()
        {
            // Arrange
            string noteName = "noteName";
            string returnNoteService = "noteReturn";

            var userServiceMock = new Mock<IUserService>();
            var noteServiceMock = new Mock<INoteService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            noteServiceMock
                .Setup(g => g.DeleteNoteAsync(noteName))
                .ReturnsAsync(returnNoteService);


            var sut = new ManagementController(userServiceMock.Object, noteServiceMock.Object, categoryServiceMock.Object);
            sut.ModelState.AddModelError("error", "error");

            // Act
            var result = await sut.DeleteNote(noteName);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task Call_NoteService_With_CorrectParams()
        {
            // Arrange
            string noteName = "noteName";
            string returnNoteService = "noteReturn";

            var userServiceMock = new Mock<IUserService>();
            var noteServiceMock = new Mock<INoteService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            noteServiceMock
                .Setup(g => g.DeleteNoteAsync(noteName))
                .ReturnsAsync(returnNoteService);


            var sut = new ManagementController(userServiceMock.Object, noteServiceMock.Object, categoryServiceMock.Object);

            // Act
            var result = await sut.DeleteNote(noteName);

            // Assert
            noteServiceMock.Verify(x => x.DeleteNoteAsync(noteName), Times.Once);
        }

        [TestMethod]
        public async Task Return_Correct_StatusCode_200()
        {
            // Arrange
            string noteName = "noteName";
            string returnNoteService = "noteReturn";

            var userServiceMock = new Mock<IUserService>();
            var noteServiceMock = new Mock<INoteService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            noteServiceMock
                .Setup(g => g.DeleteNoteAsync(noteName))
                .ReturnsAsync(returnNoteService);


            var sut = new ManagementController(userServiceMock.Object, noteServiceMock.Object, categoryServiceMock.Object);

            // Act
            var result = await sut.DeleteNote(noteName) as StatusCodeResult;

            // Assert
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public async Task Return_StatusCode_When_Exception_IsThrown()
        {
            // Arrange
            string noteName = "noteName";

            var userServiceMock = new Mock<IUserService>();
            var noteServiceMock = new Mock<INoteService>();
            var categoryServiceMock = new Mock<ICategoryService>();
            noteServiceMock
                .Setup(g => g.DeleteNoteAsync(noteName))
                .Throws<Exception>();


            var sut = new ManagementController(userServiceMock.Object, noteServiceMock.Object, categoryServiceMock.Object);

            // Act
            var result = await sut.DeleteNote(noteName) as ObjectResult;

            // Assert
            Assert.AreEqual(500, result.StatusCode);
        }
    }
}
