using HotelManagement.DataModels;
using HotelManagement.Services.Contracts;
using HotelManagement.Services.Exceptions;
using HotelManagement.Services.Wrappers.Contracts;
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
    public class PromoteUser_Should
    {
        [TestMethod]
        public async Task ThrowBadRequest_WhenModelState_IsInvalid()
        {
            var userManagerWrapperMock = new Mock<IUserManagerWrapper>();
            var userServiceMock = new Mock<IUserService>();
            var businessServiceMock = new Mock<IBusinessService>();
            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();
            var logbookServiceMock = new Mock<ILogbookService>();
            var roleManagerWrapperMock = new Mock<IRoleManagerWrapper>();
            var categoryServiceMock = new Mock<ICategoryService>();

            var model = new PromoteRoleViewModel();

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
              hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            sut.ModelState.AddModelError("error", "error");

            var result = await sut.PromoteUser(model);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        public async Task Call_BusinessService_With_Correct_Params()
        {
            var userManagerWrapperMock = new Mock<IUserManagerWrapper>();
            var userServiceMock = new Mock<IUserService>();
            var businessServiceMock = new Mock<IBusinessService>();
            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();
            var logbookServiceMock = new Mock<ILogbookService>();
            var roleManagerWrapperMock = new Mock<IRoleManagerWrapper>();
            var categoryServiceMock = new Mock<ICategoryService>();

            string businessName = "Wagamama";

            var model = new PromoteRoleViewModel()
            {
                UserId = "0510572b-de76-4f6a-ae97-76194128859f",
                RoleName = "Manager"
            };
            var user = new User()
            {
                Id = "0510572b-de76-4f6a-ae97-76194128859f"
            };
            userManagerWrapperMock
           .Setup(g => g.FindByIdAsync(model.UserId))
           .ReturnsAsync(user);

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
               hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            await sut.PromoteUser(model);

            userManagerWrapperMock.Verify(b => b.FindByIdAsync(model.UserId), Times.Once);
        }

        [TestMethod]
        public async Task Throw_WhenUser_DoesNotExist()
        {
            var userManagerWrapperMock = new Mock<IUserManagerWrapper>();
            var userServiceMock = new Mock<IUserService>();
            var businessServiceMock = new Mock<IBusinessService>();
            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();
            var logbookServiceMock = new Mock<ILogbookService>();
            var roleManagerWrapperMock = new Mock<IRoleManagerWrapper>();
            var categoryServiceMock = new Mock<ICategoryService>();

            string businessName = "Wagamama";

            var model = new PromoteRoleViewModel()
            {
                UserId = "91120f3c-57f1-421d-8e11-277c96f4c9d3",
                RoleName = "Manager"
            };
            var user = new User()
            {
                Id = "0510572b-de76-4f6a-ae97-76194128859f"
            };
            var userId = "f0b4aa93-929d-4fec-894d-1eb75ead6ba7";
            userManagerWrapperMock
           .Setup(g => g.FindByIdAsync(userId))
           .ReturnsAsync(user);

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
               hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            await Assert.ThrowsExceptionAsync<EntityInvalidException>(
                       async () => await sut.PromoteUser(model));
        }
    }
}