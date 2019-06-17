using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.Infrastructure;
using HotelManagement.Services;
using HotelManagement.Services.Exceptions;
using HotelManagement.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.ServiceTests.BusinessServiceTests
{
    [TestClass]
    public class GetBusinessByNameAsync_Should
    {
        [TestMethod]
        public async Task Throw_WhenBusiness_DoesNotExist()
        {
            var databaseName = nameof(Throw_WhenBusiness_DoesNotExist);

            var options = BusinessTestUtil.GetOptions(databaseName);

            var mappingProviderMock = new Mock<IMappingProvider>();

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new BusinessService(actAndAssertContext, mappingProviderMock.Object);
                string bizName = "InvalidBusiness";

                await Assert.ThrowsExceptionAsync<EntityInvalidException>(
                    async () => await sut.GetBusinessByNameAsync(bizName));
            }
        }

        [TestMethod]
        public async Task Return_CorrectBusiness()
        {
            var dabataseName = nameof(Return_CorrectBusiness);

            var options = BusinessTestUtil.GetOptions(dabataseName);

            // We fill the context with data and save it.
            BusinessTestUtil.FillContextWithBusinesses(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            Business business = null;
            mappingProviderMock
                .Setup(m => m.MapTo<BusinessViewModel>(It.IsAny<Business>()))
                .Callback<object>(inputargs => business = inputargs as Business);

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new BusinessService(actAndAssertContext, mappingProviderMock.Object);
                string businessName = "Hilton";

                await sut.GetBusinessByNameAsync(businessName);

                Assert.AreEqual(businessName, business.Name);
            }
        }

        [TestMethod]
        public async Task Call_MapFunction_Once()
        {
            var dabataseName = nameof(Call_MapFunction_Once);

            var options = BusinessTestUtil.GetOptions(dabataseName);

            // We fill the context with data and save it.
            BusinessTestUtil.FillContextWithBusinesses(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            Business business = null;
            mappingProviderMock
                .Setup(m => m.MapTo<BusinessViewModel>(It.IsAny<Business>()))
                .Callback<object>(inputargs => business = inputargs as Business);

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new BusinessService(actAndAssertContext, mappingProviderMock.Object);
                string businessName = "Hilton";

                await sut.GetBusinessByNameAsync(businessName);

                mappingProviderMock.Verify(m => m.MapTo<BusinessViewModel>(business), Times.Once);
            }
        }
    }
}
