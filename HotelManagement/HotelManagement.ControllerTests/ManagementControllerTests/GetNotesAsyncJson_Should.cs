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
    public class GetNotesAsyncJson_Should
    {
        [TestMethod]
        public async Task Return_Correct_JSONModel()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var noteServiceMock = new Mock<INoteService>();
            var categoryServiceMock = new Mock<ICategoryService>();

            string noteName = "noNote";
            string userIdentity = "admin@admin.admin";
            string searchByValue = "Text";

            var sut = new ManagementController(userServiceMock.Object, noteServiceMock.Object, categoryServiceMock.Object);

            // Act
            var result = await sut.GetNotesAsyncJson(noteName, userIdentity, searchByValue) as JsonResult;

            // Assert   
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public async Task CallNoteServiceOnce_OnGet()
        {
            // Arrange
            string noteName = "noNote";
            string userIdentity = "admin@admin.admin";
            string searchByValue = "Text";

            var userServiceMock = new Mock<IUserService>();
            var noteServiceMock = new Mock<INoteService>();
            noteServiceMock
            .Setup(g => g.SearchNotesAsync(noteName, userIdentity, searchByValue))
                .ReturnsAsync(new List<NoteViewModel>());
            var categoryServiceMock = new Mock<ICategoryService>();

            var sut = new ManagementController(userServiceMock.Object, noteServiceMock.Object, categoryServiceMock.Object);

            // Act
            var result = await sut.GetNotesAsyncJson(noteName, userIdentity, searchByValue) as JsonResult;

            // Assert   
            noteServiceMock.Verify(g => g.SearchNotesAsync(noteName, userIdentity, searchByValue), Times.Once);
        }
    }
}
