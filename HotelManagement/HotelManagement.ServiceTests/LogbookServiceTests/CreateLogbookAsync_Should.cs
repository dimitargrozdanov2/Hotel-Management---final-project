using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.Infrastructure;
using HotelManagement.Services;
using HotelManagement.Services.Exceptions;
using HotelManagement.ViewModels;
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
    public class CreateLogbookAsync_Should
    {
        [TestMethod]
        public async Task Throw_WhenBusiness_DoesNotExist()
        {
            var dabataseName = nameof(Throw_WhenBusiness_DoesNotExist);

            var options = LogbookTestUtil.GetOptions(dabataseName);

            LogbookTestUtil.FillContextWithLogbooks(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();

            string businessName = "Happy Bar and Grill";
            string logbookName = "Bar area";
            string description = "Happy Bar & Grill is a chain of daily restaurants in Bulgaria and Barcelona. ";

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new LogbookService(actAndAssertContext, mappingProviderMock.Object, hostingEnvironmentMock.Object); ;

                await Assert.ThrowsExceptionAsync<EntityInvalidException>(
                        async () => await sut.CreateLogbookAsync(businessName, logbookName, description));
            }
        }

        [TestMethod]
        public async Task Throw_WhenLogbook_DoesExist()
        {
            var dabataseName = nameof(Throw_WhenLogbook_DoesExist);

            var options = LogbookTestUtil.GetOptions(dabataseName);

            LogbookTestUtil.FillContextWithLogbooks(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();

            string businessName = "GROS";
            string logbookName = "Manufacturing";
            string description = "One of the core procedures in the factory is manufacturing the different kinds of pasta";

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new LogbookService(actAndAssertContext, mappingProviderMock.Object, hostingEnvironmentMock.Object); ;

                await Assert.ThrowsExceptionAsync<EntityAlreadyExistsException>(
                        async () => await sut.CreateLogbookAsync(businessName, logbookName, description));
            }
        }

        [TestMethod]
        public async Task AddNewLogbook_WhenAllParametersAreValid()
        {
            var dabataseName = nameof(AddNewLogbook_WhenAllParametersAreValid);

            var options = LogbookTestUtil.GetOptions(dabataseName);

            LogbookTestUtil.FillContextWithLogbooks(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();

            var cafeBusiness = new Business()
            {
                Name = "Caffè Nero",
                Location = "London",
                Description = "The Caffè Nero philosophy is simple: Premium award winning Italian coffee, A warm and welcoming atmosphere, Good food and great personal service."
            };

            string logbookName = "Blending";
            string description = "Blending is a part of the training for a barista.";

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Businesses.Add(cafeBusiness);

                arrangeContext.SaveChanges();
            }
            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new LogbookService(actAndAssertContext, mappingProviderMock.Object, hostingEnvironmentMock.Object); ;

                var result = await sut.CreateLogbookAsync(cafeBusiness.Name, logbookName, description);

                var business = actAndAssertContext.Businesses.FirstOrDefault(x => x.Name == cafeBusiness.Name);
                Assert.IsTrue(business.BusinessUnits.Count() == 1);
                Assert.IsTrue(actAndAssertContext.Logbooks.Any(m => m.Name == logbookName));
            }
        }

        [TestMethod]
        public async Task ReturnCorrectViewModel()
        {
            var dabataseName = nameof(ReturnCorrectViewModel);

            var options = LogbookTestUtil.GetOptions(dabataseName);

            LogbookTestUtil.FillContextWithLogbooks(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();

            mappingProviderMock.Setup(x => x.MapTo<LogbookViewModel>(It.IsAny<Logbook>())).Returns(new LogbookViewModel());

            var cafeBusiness = new Business()
            {
                Name = "Caffè Nero",
                Location = "London",
                Description = "The Caffè Nero philosophy is simple: Premium award winning Italian coffee, A warm and welcoming atmosphere, Good food and great personal service."
            };

            string logbookName = "Blending";
            string description = "Blending is a part of the training for a barista.";

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Businesses.Add(cafeBusiness);

                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new LogbookService(actAndAssertContext, mappingProviderMock.Object, hostingEnvironmentMock.Object);

                var result = await sut.CreateLogbookAsync(cafeBusiness.Name, logbookName, description);

                Assert.IsInstanceOfType(result, typeof(LogbookViewModel));
            }
        }
    }
}
