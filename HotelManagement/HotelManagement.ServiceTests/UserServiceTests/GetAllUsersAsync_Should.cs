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
                .Setup(x => x.MapTo<ICollection<UserViewModel>>(It.IsAny<List<User>>()))
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

                mappingProviderMock.Verify(m => m.MapTo<ICollection<UserViewModel>>(collectionofUsers), Times.Once);
            }
        }
    }
}