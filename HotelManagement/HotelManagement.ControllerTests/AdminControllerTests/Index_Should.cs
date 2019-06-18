using HotelManagement.Services.Contracts;
using HotelManagement.Services.Wrappers.Contracts;
using HotelManagement.ViewModels;
using HotelManagement.Web.Areas.Administration.Controllers;
using HotelManagement.Web.Areas.Administration.Models.Admin;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.ControllerTests.AdminControllerTests
{
    [TestClass]
    public class Index_Should
    {
        [TestMethod]
        public async Task Call_UserService_With_Correct_Params()
        {
            var userManagerWrapperMock = new Mock<IUserManagerWrapper>();
            var userServiceMock = new Mock<IUserService>();
            var businessServiceMock = new Mock<IBusinessService>();
            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();
            var logbookServiceMock = new Mock<ILogbookService>();
            var roleManagerWrapperMock = new Mock<IRoleManagerWrapper>();
            var categoryServiceMock = new Mock<ICategoryService>();

            var usersList = new List<UserViewModel>();

            userServiceMock
            .Setup(g => g.GetAllUsersAsync())
            .ReturnsAsync(usersList);

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
                hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            await sut.Index();

            userServiceMock.Verify(u => u.GetAllUsersAsync(), Times.Once);
        }

        [TestMethod]
        public async Task Call_RoleManagerWrapper_With_Correct_Params()
        {
            var userManagerWrapperMock = new Mock<IUserManagerWrapper>();
            var businessServiceMock = new Mock<IBusinessService>();
            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();
            var logbookServiceMock = new Mock<ILogbookService>();
            var roleManagerWrapperMock = new Mock<IRoleManagerWrapper>();
            var categoryServiceMock = new Mock<ICategoryService>();

            var usersList = new List<UserViewModel>();

            var userServiceMock = new Mock<IUserService>();
            userServiceMock
            .Setup(g => g.GetAllUsersAsync())
            .ReturnsAsync(usersList);

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
                hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            await sut.Index();

            roleManagerWrapperMock.Verify(u => u.GetAllRoles(), Times.Once);
        }

        [TestMethod]
        public async Task ReturnCorrectViewModel_OnGet()
        {
            var userManagerWrapperMock = new Mock<IUserManagerWrapper>();
            var userServiceMock = new Mock<IUserService>();
            var businessServiceMock = new Mock<IBusinessService>();
            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();
            var logbookServiceMock = new Mock<ILogbookService>();
            var roleManagerWrapperMock = new Mock<IRoleManagerWrapper>();
            var categoryServiceMock = new Mock<ICategoryService>();

            var usersList = new List<UserViewModel>();

            userServiceMock
            .Setup(g => g.GetAllUsersAsync())
            .ReturnsAsync(usersList);

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
                hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            var result = await sut.Index() as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(ListUsersViewModel));
        }
    }
}