using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.Infrastructure;
using HotelManagement.Services;
using HotelManagement.Services.Wrappers.Contracts;
using HotelManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.ServiceTests.LogbookServiceTests
{
    [TestClass]
    public class GetLogBooksForBusiness_Should
    {
        [TestMethod]
        public async Task Call_MapFunction_Once()
        {
            var dabataseName = nameof(Call_MapFunction_Once);

            var options = LogbookTestUtil.GetOptions(dabataseName);

            var collectionofUsers = new List<Logbook>();

            var userManagerWrapperMock = new Mock<IUserManagerWrapper>();

            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();

            var mappingProviderMock = new Mock<IMappingProvider>();

            mappingProviderMock
                .Setup(x => x.MapTo<ICollection<LogbookViewModel>>(It.IsAny<List<Logbook>>()))
                .Callback<object>(inputargs => collectionofUsers = inputargs as List<Logbook>);

            string businessName = "GROS";

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new LogbookService(actAndAssertContext, mappingProviderMock.Object, hostingEnvironmentMock.Object);
                await sut.GetLogBooksForBusiness(businessName);

                mappingProviderMock.Verify(m => m.MapTo<ICollection<LogbookViewModel>>(collectionofUsers), Times.Once);
            }
        }
    }
}
