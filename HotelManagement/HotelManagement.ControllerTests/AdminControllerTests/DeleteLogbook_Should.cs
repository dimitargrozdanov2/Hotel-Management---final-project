using HotelManagement.Services.Contracts;
using HotelManagement.Services.Wrappers.Contracts;
using HotelManagement.ViewModels;
using HotelManagement.Web.Areas.Administration.Controllers;
using HotelManagement.Web.Areas.Administration.Models.Admin;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace HotelManagement.ControllerTests.AdminControllerTests
{
    [TestClass]
    public class DeleteLogbook_Should
    {
        [TestMethod]
        public async Task ReturnCorrectTypeofViewResult_OnGet()
        {
            var userManagerWrapperMock = new Mock<IUserManagerWrapper>();
            var userServiceMock = new Mock<IUserService>();
            var businessServiceMock = new Mock<IBusinessService>();
            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();
            var logbookServiceMock = new Mock<ILogbookService>();
            var roleManagerWrapperMock = new Mock<IRoleManagerWrapper>();
            var categoryServiceMock = new Mock<ICategoryService>();

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
                hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            string logbookName = "Reception";
            var result = sut.DeleteLogbook(logbookName);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task ReturnCorrectTypeofViewResult_WhenModelState_IsInvalid()
        {
            var userManagerWrapperMock = new Mock<IUserManagerWrapper>();
            var userServiceMock = new Mock<IUserService>();
            var businessServiceMock = new Mock<IBusinessService>();
            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();
            var logbookServiceMock = new Mock<ILogbookService>();
            var roleManagerWrapperMock = new Mock<IRoleManagerWrapper>();
            var categoryServiceMock = new Mock<ICategoryService>();

            var model = new DeleteLogbookViewModel();

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
                hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            sut.ModelState.AddModelError("error", "error");
            var result = await sut.DeleteLogbook(model);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Call_LoogbookService_With_Correct_Params()
        {
            var userManagerWrapperMock = new Mock<IUserManagerWrapper>();
            var userServiceMock = new Mock<IUserService>();
            var businessServiceMock = new Mock<IBusinessService>();
            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();
            var logbookServiceMock = new Mock<ILogbookService>();
            var roleManagerWrapperMock = new Mock<IRoleManagerWrapper>();
            var categoryServiceMock = new Mock<ICategoryService>();

            string logbookName = "Kitchen";

            var model = new LogbookViewModel
            {
                Id = "c5cfcf33-3512-401c-adff-cc397fa7def8",
                Name = "To do",
                CreatedOn = DateTime.Now
            };

            var deletelogbookModel = new DeleteLogbookViewModel()
            {
                Name = logbookName,
            };

            logbookServiceMock
           .Setup(g => g.DeleteLogbook(logbookName))
           .ReturnsAsync(model);

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
               hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            await sut.DeleteLogbook(deletelogbookModel);

            logbookServiceMock.Verify(b => b.DeleteLogbook(logbookName), Times.Once);
        }

        [TestMethod]
        public async Task RedirectToCorrectAction_WhenModelState_IsValid()
        {
            var userManagerWrapperMock = new Mock<IUserManagerWrapper>();
            var userServiceMock = new Mock<IUserService>();
            var businessServiceMock = new Mock<IBusinessService>();
            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();
            var logbookServiceMock = new Mock<ILogbookService>();
            var roleManagerWrapperMock = new Mock<IRoleManagerWrapper>();
            var categoryServiceMock = new Mock<ICategoryService>();

            string logbookName = "Kitchen";

            var model = new LogbookViewModel
            {
                Id = "c5cfcf33-3512-401c-adff-cc397fa7def8",
                Name = "To do",
                CreatedOn = DateTime.Now
            };

            var deletelogbookModel = new DeleteLogbookViewModel()
            {
                Name = logbookName,
            };

            logbookServiceMock
           .Setup(g => g.DeleteLogbook(logbookName))
           .ReturnsAsync(model);

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
               hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            var result = await sut.DeleteLogbook(deletelogbookModel);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirect = (RedirectToActionResult)result;

            Assert.IsTrue(redirect.ControllerName == "Admin");
            Assert.IsTrue(redirect.ActionName == "AllBusinesses");
        }
    }
}