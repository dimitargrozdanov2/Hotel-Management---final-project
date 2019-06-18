using HotelManagement.Services.Contracts;
using HotelManagement.Services.Wrappers.Contracts;
using HotelManagement.ViewModels;
using HotelManagement.Web.Areas.Administration.Controllers;
using HotelManagement.Web.Areas.Administration.Models.Admin;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.ControllerTests.AdminControllerTests
{
    [TestClass]
    public class AddImageToBusiness_Should
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
            var result = sut.AddImageToBusiness(businessName);

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

            var model = new AddImageToBusinessViewModel();

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
                hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            sut.ModelState.AddModelError("error", "error");
            var result = await sut.AddImageToBusiness(model);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        //[TestMethod]
        //public async Task Call_BusinessService_With_Correct_Params()
        //{
        //    var userManagerWrapperMock = new Mock<IUserManagerWrapper>();
        //    var userServiceMock = new Mock<IUserService>();
        //    var businessServiceMock = new Mock<IBusinessService>();
        //    var hostingEnvironmentMock = new Mock<IHostingEnvironment>();
        //    var logbookServiceMock = new Mock<ILogbookService>();
        //    var roleManagerWrapperMock = new Mock<IRoleManagerWrapper>();
        //    var categoryServiceMock = new Mock<ICategoryService>();


        //    //var ms = new MemoryStream();
        //    //var formFile = new FormFile(ms, 0, ms.Length, "name", imageNameToSave);
        //    string businessName = "Wagamama";
        //    string imageUrl = "Wagamama_logo";
        //    string businessId = "4caae5e7-efe6-42b7-ad5e-1bea876ab069";
        //    IFormFile Image = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.jpg");

        //    businessServiceMock.Setup(f => f.AddImageToBusiness(businessName, imageUrl, Image)).ReturnsAsync(It.IsAny<BusinessViewModel>());

        //    var currDir = Directory.GetCurrentDirectory();
        //    var startIndex = currDir.IndexOf(@"\HotelManagement.ControllerTests\");
        //    var folderImagesUsedForTests = "\\HotelManagement.ControllerTests\\ImagesUsedForTests";
        //    var path = Path.Combine(currDir.Substring(0, startIndex) + folderImagesUsedForTests);

        //    hostingEnvironmentMock.SetupGet(x => x.WebRootPath).Returns(path);

        //    var model = new BusinessViewModel()
        //    {
        //        Name = businessName,
        //        Id = businessId,
        //        CreatedOn = DateTime.Now,
        //        Location = "London",
        //        Description = "Wagamama is a British restaurant chain, serving Asian food based on Japanese cuisine"
        //    };


        //    var addImageModel = new AddImageToBusinessViewModel()
        //    {
        //        name = businessName,
        //        ImageName = imageUrl,
        //        Image = Image
        //    };
        //    businessServiceMock
        //   .Setup(g => g.AddImageToBusiness(businessName, imageUrl, Image))
        //   .ReturnsAsync(model);

        //    var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
        //       hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

        //    await sut.AddImageToBusiness(addImageModel);

        //    businessServiceMock.Verify(b => b.AddImageToBusiness(businessName, imageUrl, Image), Times.Once);

        //    File.Delete(path + $"\\Images\\Project\\{imageUrl}.jpg");
        //}

        //    [TestMethod]
        //    public async Task RedirectToCorrectAction_WhenModelState_IsValid()
        //    {
        //        var userManagerWrapperMock = new Mock<IUserManagerWrapper>();
        //        var userServiceMock = new Mock<IUserService>();
        //        var businessServiceMock = new Mock<IBusinessService>();
        //        var hostingEnvironmentMock = new Mock<IHostingEnvironment>();
        //        var logbookServiceMock = new Mock<ILogbookService>();
        //        var roleManagerWrapperMock = new Mock<IRoleManagerWrapper>();
        //        var categoryServiceMock = new Mock<ICategoryService>();

        //        string businessName = "Wagamama";
        //        string imageUrl = "Wagamama_logo";
        //        IFormFile Image = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.jpg");
        //        var currDir = Directory.GetCurrentDirectory();
        //        var startIndex = currDir.IndexOf(@"\HotelManagement.ControllerTests\");
        //        var folderImagesUsedForTests = "\\HotelManagement.ControllerTests\\ImagesUsedForTests";
        //        var path = Path.Combine(currDir.Substring(0, startIndex) + folderImagesUsedForTests);

        //        hostingEnvironmentMock.SetupGet(x => x.WebRootPath).Returns(path);

        //        var model = new BusinessViewModel()
        //        {
        //            Name = businessName
        //        };

        //        var addImageModel = new AddImageToBusinessViewModel()
        //        {
        //            name = businessName,
        //            ImageName = imageUrl,
        //            Image = Image
        //        };

        //        businessServiceMock
        //       .Setup(g => g.AddImageToBusiness(businessName, imageUrl, Image))
        //       .ReturnsAsync(model);

        //        var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
        //           hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

        //        var result = await sut.AddImageToBusiness(addImageModel);

        //        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        //        var redirect = (RedirectToActionResult)result;

        //        Assert.IsTrue(redirect.ControllerName == "Admin");
        //        Assert.IsTrue(redirect.ActionName == "AllBusinesses");

        //        File.Delete(path + $"\\Images\\Project\\{imageUrl}.jpg");
        //    }
        //}
    }
}
