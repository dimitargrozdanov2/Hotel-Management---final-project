using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.Infrastructure;
using HotelManagement.Services;
using HotelManagement.Services.Exceptions;
using HotelManagement.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.ServiceTests.BusinessServiceTests
{
    [TestClass]
    public class CreateBusinessAsync_Should
    {
        [TestMethod]
        public async Task Throw_WhenBusiness_DoesExist()
        {
            var dabataseName = nameof(Throw_WhenBusiness_DoesExist);

            var options = BusinessTestUtil.GetOptions(dabataseName);

            BusinessTestUtil.FillContextWithBusinesses(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            string businessName = "GROS";
            string location = "Sofia";
            string description = "This factory is building a head office in Sofia soon";

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new BusinessService(actAndAssertContext, mappingProviderMock.Object); ;

                await Assert.ThrowsExceptionAsync<EntityAlreadyExistsException>(
                        async () => await sut.CreateBusinessAsync(businessName, location, description));
            }
        }

        [TestMethod]
        public async Task AddNewBusiness_WhenAllParametersAreValid()
        {
            var dabataseName = nameof(AddNewBusiness_WhenAllParametersAreValid);

            var options = BusinessTestUtil.GetOptions(dabataseName);

            var mappingProviderMock = new Mock<IMappingProvider>();

            string businessName = "Compass";
            string location = "village Komarevo";
            string description = "Compass produces meat";

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                var sut = new BusinessService(arrangeContext, mappingProviderMock.Object);

                await sut.CreateBusinessAsync(businessName, location, description);

                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                Assert.IsTrue(actAndAssertContext.Businesses.Count() == 1);
                Assert.IsTrue(actAndAssertContext.Businesses.Any(m => m.Name == businessName));
            }
        }

        [TestMethod]
        public async Task ReturnCorrectViewModel()
        {
            var dabataseName = nameof(ReturnCorrectViewModel);

            var options = BusinessTestUtil.GetOptions(dabataseName);

            var mappingProviderMock = new Mock<IMappingProvider>();

            mappingProviderMock.Setup(x => x.MapTo<BusinessViewModel>(It.IsAny<Business>())).Returns(new BusinessViewModel());

            string businessName = "Compass";
            string location = "village Komarevo";
            string description = "Compass produces meat";

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new BusinessService(actAndAssertContext, mappingProviderMock.Object);

                var result = await sut.CreateBusinessAsync(businessName, location, description);

                Assert.IsInstanceOfType(result, typeof(BusinessViewModel));
            }
        }
    }
}