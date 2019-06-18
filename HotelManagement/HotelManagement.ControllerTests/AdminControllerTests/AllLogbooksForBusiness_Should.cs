using HotelManagement.Services.Contracts;
using HotelManagement.Services.Wrappers.Contracts;
using HotelManagement.ViewModels;
using HotelManagement.Web.Areas.Administration.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.ControllerTests.AdminControllerTests
{
    [TestClass]
    public class AllLogbooksForBusiness_Should
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

            var logbookList = new List<LogbookViewModel>();
            string businessName = "Shell";

            logbookServiceMock
            .Setup(g => g.GetLogBooksForBusiness(businessName))
            .ReturnsAsync(logbookList);

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
                hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            await sut.AllLogbooksForBusiness(businessName);

            logbookServiceMock.Verify(b => b.GetLogBooksForBusiness(businessName), Times.Once);
        }

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

            var logbookList = new List<LogbookViewModel>();
            string businessName = "Shell";

            logbookServiceMock
            .Setup(g => g.GetLogBooksForBusiness(businessName))
            .ReturnsAsync(logbookList);

            var sut = new AdminController(userManagerWrapperMock.Object, userServiceMock.Object, businessServiceMock.Object,
                hostingEnvironmentMock.Object, logbookServiceMock.Object, roleManagerWrapperMock.Object, categoryServiceMock.Object);

            var result = await sut.AllLogbooksForBusiness(businessName) as ViewResult;

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}