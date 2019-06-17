using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.Infrastructure;
using HotelManagement.Services;
using HotelManagement.Services.Exceptions;
using HotelManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.ServiceTests.LogbookServiceTests
{
    [TestClass]
    public class DeleteLogbook_Should
    {
        [TestMethod]
        public async Task Throw_WhenLogbook_DoesExist()
        {
            var dabataseName = nameof(Throw_WhenLogbook_DoesExist);

            var options = LogbookTestUtil.GetOptions(dabataseName);

            LogbookTestUtil.FillContextWithLogbooks(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();

            string logbookName = "Bar area";

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new LogbookService(actAndAssertContext, mappingProviderMock.Object, hostingEnvironmentMock.Object); ;

                await Assert.ThrowsExceptionAsync<EntityInvalidException>(
                        async () => await sut.DeleteLogbook(logbookName));
            }
        }

        [TestMethod]
        public async Task DeleteLogbook_WhenAllParametersAreValid()
        {
            var dabataseName = nameof(DeleteLogbook_WhenAllParametersAreValid);

            var options = LogbookTestUtil.GetOptions(dabataseName);

            LogbookTestUtil.FillContextWithLogbooks(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();

            string logbookName = "Manufacturing";

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new LogbookService(actAndAssertContext, mappingProviderMock.Object, hostingEnvironmentMock.Object); ;

                await sut.DeleteLogbook(logbookName);

                var logbook = await actAndAssertContext.Logbooks.FirstOrDefaultAsync(l => l.Name == logbookName);
                Assert.IsTrue(logbook == null);
            }
        }

        [TestMethod]
        public async Task ReturnCorrectViewModel()
        {
            var dabataseName = nameof(ReturnCorrectViewModel);

            var options = LogbookTestUtil.GetOptions(dabataseName);

            LogbookTestUtil.FillContextWithLogbooks(options);

            string logbookName = "Manufacturing";

            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();

            var mappingProviderMock = new Mock<IMappingProvider>();
            mappingProviderMock.Setup(x => x.MapTo<LogbookViewModel>(It.IsAny<Logbook>())).Returns(new LogbookViewModel());

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new LogbookService(actAndAssertContext, mappingProviderMock.Object, hostingEnvironmentMock.Object);

                var result = await sut.DeleteLogbook(logbookName);

                Assert.IsInstanceOfType(result, typeof(LogbookViewModel));
            }
        }
    }
}

