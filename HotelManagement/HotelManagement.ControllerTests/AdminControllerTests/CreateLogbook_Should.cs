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
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.ControllerTests.AdminControllerTests
{
    [TestClass]
    public class CreateLogbook_Should
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

            string businessName = "Shell";
            var result = sut.CreateLogbook();

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

            var model = new CreateLogbookViewModel();
            string businessName = "Wagamama";

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
                hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            sut.ModelState.AddModelError("error", "error");
            var result = await sut.CreateLogbook(businessName,model);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Call_LogbookService_With_Correct_Params()
        {
            var userManagerWrapperMock = new Mock<IUserManagerWrapper>();
            var userServiceMock = new Mock<IUserService>();
            var businessServiceMock = new Mock<IBusinessService>();
            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();
            var logbookServiceMock = new Mock<ILogbookService>();
            var roleManagerWrapperMock = new Mock<IRoleManagerWrapper>();
            var categoryServiceMock = new Mock<ICategoryService>();

            string businessName = "Wagamama";

            var model = new LogbookViewModel()
            {
                Name = businessName
            };


            var addImageModel = new CreateLogbookViewModel()
            {
                Name = businessName,
                Description = "Wagamama is a British restaurant chain, serving Asian food based on Japanese cuisine.",
            };

            logbookServiceMock
           .Setup(g => g.CreateLogbookAsync(businessName, addImageModel.Name, addImageModel.Description))
           .ReturnsAsync(model);

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
               hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            await sut.CreateLogbook(businessName, addImageModel);

            logbookServiceMock.Verify(b => b.CreateLogbookAsync(businessName, addImageModel.Name, addImageModel.Description), Times.Once);
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

            string businessName = "Wagamama";
          
            var model = new LogbookViewModel()
            {
                Name = businessName
            };


            var addImageModel = new CreateLogbookViewModel()
            {
                Name = businessName,
                Description = "Wagamama is a British restaurant chain, serving Asian food based on Japanese cuisine.",
            };

            logbookServiceMock
           .Setup(g => g.CreateLogbookAsync(businessName, addImageModel.Name, addImageModel.Description))
           .ReturnsAsync(model);

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
               hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            var result = await sut.CreateLogbook(businessName, addImageModel);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirect = (RedirectToActionResult)result;

            Assert.IsTrue(redirect.ControllerName == "Admin");
            Assert.IsTrue(redirect.ActionName == "AllLogbooksForBusiness");
        }
    }
}
