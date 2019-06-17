using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.Infrastructure;
using HotelManagement.Services;
using HotelManagement.Services.Wrappers.Contracts;
using HotelManagement.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.ServiceTests.UserServiceTests
{
    [TestClass]
    public class GetAllUsersAsync_Should
    {
        [TestMethod]
        public async Task Call_MapFunction_Once()
        {
            var dabataseName = nameof(Call_MapFunction_Once);

            var options = UserTestUtil.GetOptions(dabataseName);

            var collectionofUsers = new List<User>();

            var userManagerWrapperMock = new Mock<IUserManagerWrapper>();

            var mappingProviderMock = new Mock<IMappingProvider>();

            mappingProviderMock
                .Setup(x => x.MapTo<IEnumerable<UserViewModel>>(It.IsAny<List<User>>()))
                .Callback<object>(inputargs => collectionofUsers = inputargs as List<User>);

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Users.Add(new User()
                {
                    UserName = "groz@admin.admin"
                });
                arrangeContext.Users.Add(new User()
                {
                    UserName = "grozdeto@admin.admin"
                });
                await arrangeContext.SaveChangesAsync();
            }

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new UserService(actAndAssertContext, mappingProviderMock.Object, userManagerWrapperMock.Object);
                await sut.GetAllUsersAsync();

                mappingProviderMock.Verify(m => m.MapTo<IEnumerable<UserViewModel>>(collectionofUsers), Times.Once);
            }
        }

        [TestMethod]
        public async Task Return_Users_Successfully()
        {
            var databaseName = nameof(Return_Users_Successfully);

            var options = UserTestUtil.GetOptions(databaseName);

            UserTestUtil.FillContextWithUserData(options);

            var users = new List<User>();

            var userManagerWrapperMock = new Mock<IUserManagerWrapper>();
            var mappingProviderMock = new Mock<IMappingProvider>();
            mappingProviderMock
                .Setup(x => x.MapTo<IEnumerable<UserViewModel>>(It.IsAny<List<User>>()))
                .Callback<object>(inputargs => users = inputargs as List<User>);

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new UserService(actAndAssertContext, mappingProviderMock.Object, userManagerWrapperMock.Object);

                var returnUsers = await sut.GetAllUsersAsync();

                Assert.IsTrue(users.Count() == 1);
            }
        }
    }
}