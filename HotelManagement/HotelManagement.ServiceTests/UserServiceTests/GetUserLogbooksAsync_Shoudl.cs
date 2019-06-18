using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.Infrastructure;
using HotelManagement.Services;
using HotelManagement.Services.Wrappers.Contracts;
using HotelManagement.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.ServiceTests.UserServiceTests
{
    [TestClass]
    public class GetUserLogbooksAsync_Shoudl
    {
        [TestMethod]
        public async Task Return_UserLogbooks_Successfully()
        {
            var databaseName = nameof(Return_UserLogbooks_Successfully);

            var options = UserTestUtil.GetOptions(databaseName);

            UserTestUtil.FillContextWithUserData(options);

            var logbooks = new List<Logbook>();

            var mappingProviderMock = new Mock<IMappingProvider>();
            mappingProviderMock
                .Setup(x => x.MapTo<IEnumerable<LogbookViewModel>>(It.IsAny<List<Logbook>>()))
                .Callback<object>(inputargs => logbooks = inputargs as List<Logbook>);

            var userManagerWrapper = new Mock<IUserManagerWrapper>();

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new UserService(actAndAssertContext, mappingProviderMock.Object, userManagerWrapper.Object);

                await sut.GetUserLogbooksAsync("admin@admin.admin");

                Assert.IsTrue(logbooks.Count() == 1);
            }
        }

        [TestMethod]
        public async Task Call_MappingFunction_Once()
        {
            var databaseName = nameof(Return_UserLogbooks_Successfully);

            var options = UserTestUtil.GetOptions(databaseName);

            UserTestUtil.FillContextWithUserData(options);

            var logbooks = new List<Logbook>();

            var mappingProviderMock = new Mock<IMappingProvider>();
            mappingProviderMock
                .Setup(x => x.MapTo<IEnumerable<LogbookViewModel>>(It.IsAny<List<Logbook>>()))
                .Callback<object>(inputargs => logbooks = inputargs as List<Logbook>);

            var userManagerWrapper = new Mock<IUserManagerWrapper>();

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new UserService(actAndAssertContext, mappingProviderMock.Object, userManagerWrapper.Object);

                await sut.GetUserLogbooksAsync("admin@admin.admin");

                mappingProviderMock.Verify(m => m.MapTo<IEnumerable<LogbookViewModel>>(logbooks), Times.Once);
            }
        }
    }
}