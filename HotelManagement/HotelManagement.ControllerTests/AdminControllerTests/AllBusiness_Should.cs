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
    public class AllBusiness_Should
    {
        [TestMethod]
        public async Task Call_BusinessService_With_Correct_Params()
        {
            var userManagerWrapperMock = new Mock<IUserManagerWrapper>();
            var userServiceMock = new Mock<IUserService>();
            var businessServiceMock = new Mock<IBusinessService>();
            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();
            var logbookServiceMock = new Mock<ILogbookService>();
            var roleManagerWrapperMock = new Mock<IRoleManagerWrapper>();
            var categoryServiceMock = new Mock<ICategoryService>();

            var businessList = new List<BusinessViewModel>();
            string key = "date";

            businessServiceMock
            .Setup(g => g.GetBusinesses(key, true))
            .ReturnsAsync(businessList);

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
                hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            await sut.AllBusinesses();

            businessServiceMock.Verify(b => b.GetBusinesses(key, true), Times.Once);
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

            var businessList = new List<BusinessViewModel>();
            string key = "date";

            businessServiceMock
            .Setup(g => g.GetBusinesses(key, true))
            .ReturnsAsync(businessList);

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
                hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            var result = await sut.AllBusinesses() as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(ListBusinessesViewModel));
        }
    }
}