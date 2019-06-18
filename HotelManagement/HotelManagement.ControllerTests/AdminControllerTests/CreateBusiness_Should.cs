using HotelManagement.Services.Contracts;
using HotelManagement.Services.Exceptions;
using HotelManagement.Services.Wrappers.Contracts;
using HotelManagement.ViewModels;
using HotelManagement.Web.Areas.Administration.Controllers;
using HotelManagement.Web.Areas.Administration.Models.Admin;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.ControllerTests.AdminControllerTests
{
    [TestClass]
    public class CreateBusiness_Should
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

            var model = new CreateBusinessViewModel();

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
              hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            sut.ModelState.AddModelError("error", "error");

            var result = await sut.CreateBusiness(model);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task Call_BusinessService_With_Correct_Params()
        {
            // Arrange
            var model = new CreateBusinessViewModel();
            model.Name = "Starbucks";
            model.Location = "Seattle";
            model.Description = "Starbucks locations serve hot and cold drinks, whole-bean coffee";

            var userManagerWrapperMock = new Mock<IUserManagerWrapper>();
            var userServiceMock = new Mock<IUserService>();
            var businessServiceMock = new Mock<IBusinessService>();
            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();
            var logbookServiceMock = new Mock<ILogbookService>();
            var roleManagerWrapperMock = new Mock<IRoleManagerWrapper>();
            var categoryServiceMock = new Mock<ICategoryService>();

            businessServiceMock.Setup(f => f.CreateBusinessAsync(model.Name, model.Location, model.Description)).ReturnsAsync(It.IsAny<BusinessViewModel>());

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
             hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);
            // Act
            var result = await sut.CreateBusiness(model);

            // Assert
            businessServiceMock.Verify(x => x.CreateBusinessAsync(model.Name, model.Location, model.Description), Times.Once);
        }

        [TestMethod]
        public async Task Return_Json()
        {
            var userManagerWrapperMock = new Mock<IUserManagerWrapper>();
            var userServiceMock = new Mock<IUserService>();
            var businessServiceMock = new Mock<IBusinessService>();
            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();
            var logbookServiceMock = new Mock<ILogbookService>();
            var roleManagerWrapperMock = new Mock<IRoleManagerWrapper>();
            var categoryServiceMock = new Mock<ICategoryService>();

            var model = new CreateBusinessViewModel();
            model.Name = "Starbucks";
            model.Location = "Seattle";
            model.Description = "Starbucks locations serve hot and cold drinks, whole-bean coffee";

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
             hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            var result = await sut.CreateBusiness(model) as JsonResult;

            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public async Task Return_StatusCode400_When_Exception_IsThrown()
        { 
            var model = new CreateBusinessViewModel();
            model.Name = "Starbucks";
            model.Location = "Seattle";
            model.Description = "Starbucks locations serve hot and cold drinks, whole-bean coffee";

            var userManagerWrapperMock = new Mock<IUserManagerWrapper>();
            var userServiceMock = new Mock<IUserService>();
            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();
            var logbookServiceMock = new Mock<ILogbookService>();
            var roleManagerWrapperMock = new Mock<IRoleManagerWrapper>();
            var categoryServiceMock = new Mock<ICategoryService>();

            var businessServiceMock = new Mock<IBusinessService>();
            businessServiceMock.Setup(b => b.CreateBusinessAsync(model.Name, model.Location, model.Description)).Throws<EntityAlreadyExistsException>();

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
             hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);
 
            var result = await sut.CreateBusiness(model) as ObjectResult;

            Assert.AreEqual(400, result.StatusCode);

        }
    }
}
