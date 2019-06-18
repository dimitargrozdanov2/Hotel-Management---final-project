using HotelManagement.Services.Contracts;
using HotelManagement.Services.Wrappers.Contracts;
using HotelManagement.ViewModels;
using HotelManagement.Web.Areas.Administration.Controllers;
using HotelManagement.Web.Areas.Administration.Models.Admin;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace HotelManagement.ControllerTests.AdminControllerTests
{
    [TestClass]
    public class ManageManager_Should
    {
        [TestMethod]
        public void ReturnCorrectTypeofViewResult_OnGet()
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

            string businessName = "Shell";
            var result = sut.ManageManager(businessName) as ViewResult;

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

            var model = new ManagerManageViewModel();
            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
                hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            sut.ModelState.AddModelError("error", "error");
            var result = await sut.ManageManager(model);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Call_LogboookService_With_Correct_Params()
        {
            var userManagerWrapperMock = new Mock<IUserManagerWrapper>();
            var userServiceMock = new Mock<IUserService>();
            var businessServiceMock = new Mock<IBusinessService>();
            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();
            var logbookServiceMock = new Mock<ILogbookService>();
            var roleManagerWrapperMock = new Mock<IRoleManagerWrapper>();
            var categoryServiceMock = new Mock<ICategoryService>();

            string logbookName = "Kitchen";

            var model = new LogbookViewModel()
            {
                Name = logbookName
            };


            var managerModel = new ManagerManageViewModel()
            {
                LogbookName = logbookName,
                ManagerEmail = "adminKitchen@admin.admin",
            };

            logbookServiceMock
           .Setup(g => g.ManageManagerAsync(managerModel.LogbookName, managerModel.ManagerEmail))
           .ReturnsAsync(model);

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
               hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            await sut.ManageManager(managerModel);

            logbookServiceMock.Verify(b => b.ManageManagerAsync(managerModel.LogbookName, managerModel.ManagerEmail), Times.Once);
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

            var model = new LogbookViewModel()
            {
                Name = logbookName
            };


            var managerModel = new ManagerManageViewModel()
            {
                LogbookName = logbookName,
                ManagerEmail = "adminKitchen@admin.admin",
            };

            logbookServiceMock
           .Setup(g => g.ManageManagerAsync(managerModel.LogbookName, managerModel.ManagerEmail))
           .ReturnsAsync(model);

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
               hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            var result = await sut.ManageManager(managerModel);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirect = (RedirectToActionResult)result;

            Assert.IsTrue(redirect.ControllerName == "Admin");
            Assert.IsTrue(redirect.ActionName == "AllBusinesses");
        }
    }
}
