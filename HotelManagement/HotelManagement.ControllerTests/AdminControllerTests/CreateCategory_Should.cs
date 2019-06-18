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
    public class CreateCategory_Should
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
            var result = sut.CreateCategory(logbookName);

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

            var model = new CreateCategoryViewModel();
            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
                hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            sut.ModelState.AddModelError("error", "error");
            var result = await sut.CreateCategory(model);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Call_CategoryService_With_Correct_Params()
        {
            var userManagerWrapperMock = new Mock<IUserManagerWrapper>();
            var userServiceMock = new Mock<IUserService>();
            var businessServiceMock = new Mock<IBusinessService>();
            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();
            var logbookServiceMock = new Mock<ILogbookService>();
            var roleManagerWrapperMock = new Mock<IRoleManagerWrapper>();
            var categoryServiceMock = new Mock<ICategoryService>();

            string logbookName = "Kitchen";

            var model = new CategoryViewModel
            {
                Id = "c5cfcf33-3512-401c-adff-cc397fa7def8",
                Name = "To do",
                CreatedOn = DateTime.Now
            };

            var categoryModel = new CreateCategoryViewModel()
            {
                LogbookName = logbookName,
                CategoryName = "ChefTeam",
            };

            categoryServiceMock
           .Setup(g => g.CreateCategoryAsync(categoryModel.CategoryName, categoryModel.LogbookName))
           .ReturnsAsync(model);

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
               hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            await sut.CreateCategory(categoryModel);

            categoryServiceMock.Verify(b => b.CreateCategoryAsync(categoryModel.CategoryName, categoryModel.LogbookName), Times.Once);
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

            var model = new CategoryViewModel
            {
                Id = "c5cfcf33-3512-401c-adff-cc397fa7def8",
                Name = "To do",
                CreatedOn = DateTime.Now
            };

            var categoryModel = new CreateCategoryViewModel()
            {
                LogbookName = logbookName,
                CategoryName = "ChefTeam",
            };

            categoryServiceMock
           .Setup(g => g.CreateCategoryAsync(categoryModel.CategoryName, categoryModel.LogbookName))
           .ReturnsAsync(model);

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
               hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            var result = await sut.CreateCategory(categoryModel);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirect = (RedirectToActionResult)result;

            Assert.IsTrue(redirect.ControllerName == "Admin");
            Assert.IsTrue(redirect.ActionName == "AllBusinesses");
        }
    }
}