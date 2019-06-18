using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.Infrastructure;
using HotelManagement.Services;
using HotelManagement.Services.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.ServiceTests.LogbookServiceTests
{
    [TestClass]
    public class ManageManagerAsync_Should
    {
        [TestMethod]
        public async Task Throw_WhenLogbook_DoesNotExist()
        {
            var dabataseName = nameof(Throw_WhenLogbook_DoesNotExist);

            var options = LogbookTestUtil.GetOptions(dabataseName);

            var mappingProviderMock = new Mock<IMappingProvider>();

            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();

            string logbookName = "Restaurant";

            string managerEmail = "admin@admin.admin";

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new LogbookService(actAndAssertContext, mappingProviderMock.Object, hostingEnvironmentMock.Object); ;

                await Assert.ThrowsExceptionAsync<EntityInvalidException>(
                        async () => await sut.ManageManagerAsync(logbookName, managerEmail));
            }
        }

        [TestMethod]
        public async Task Throw_WhenManager_DoesNotExist()
        {
            var dabataseName = nameof(Throw_WhenManager_DoesNotExist);

            var options = LogbookTestUtil.GetOptions(dabataseName);

            LogbookTestUtil.FillContextWithLogbooks(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();

            string logbookName = "Manufacturing";

            string managerEmail = "admin@admin.admin";

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new LogbookService(actAndAssertContext, mappingProviderMock.Object, hostingEnvironmentMock.Object); ;

                await Assert.ThrowsExceptionAsync<EntityInvalidException>(
                        async () => await sut.ManageManagerAsync(logbookName, managerEmail));
            }
        }

        [TestMethod]
        public async Task Throw_WhenUser_IsNotManager()
        {
            var dabataseName = nameof(Throw_WhenManager_DoesNotExist);

            var options = LogbookTestUtil.GetOptions(dabataseName);

            LogbookTestUtil.FillContextWithLogbooks(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();

            string logbookName = "Manufacturing";

            string userEmail = "dimitar.pasta@admin.admin";

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {

                var sut = new LogbookService(actAndAssertContext, mappingProviderMock.Object, hostingEnvironmentMock.Object); ;

                await Assert.ThrowsExceptionAsync<EntityInvalidException>(
                        async () => await sut.ManageManagerAsync(logbookName, userEmail));
            }
        }
    }
}

