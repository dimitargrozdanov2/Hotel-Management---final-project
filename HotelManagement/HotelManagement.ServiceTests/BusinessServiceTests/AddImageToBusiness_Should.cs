using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.Infrastructure;
using HotelManagement.Services;
using HotelManagement.Services.Exceptions;
using HotelManagement.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.ServiceTests.BusinessServiceTests
{
    [TestClass]
    public class AddImageToBusiness_Should
    {
        [TestMethod]
        public async Task Throw_WhenBusiness_DoesNotExist()
        {
            var dabataseName = nameof(Throw_WhenBusiness_DoesNotExist);

            var options = BusinessTestUtil.GetOptions(dabataseName);

            BusinessTestUtil.FillContextWithBusinesses(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            string businessName = "Wagamama";
            string imageUrl = "Wagamama_logo.jpg";
            IFormFile Image = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.jpg");

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new BusinessService(actAndAssertContext, mappingProviderMock.Object);

                await Assert.ThrowsExceptionAsync<EntityInvalidException>(
                        async () => await sut.AddImageToBusiness(businessName, imageUrl, Image));
            }
        }

        [TestMethod]
        public async Task Throw_WhenLoogbook_DoesNotExist()
        {
            var dabataseName = nameof(Throw_WhenLoogbook_DoesNotExist);

            var options = BusinessTestUtil.GetOptions(dabataseName);

            BusinessTestUtil.FillContextWithBusinesses(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            string businessName = "Hilton";
            string imageUrl = "Hilton_picture.jpg";
            IFormFile Image = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.jpg");

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new BusinessService(actAndAssertContext, mappingProviderMock.Object);

                await Assert.ThrowsExceptionAsync<EntityInvalidException>(
                        async () => await sut.AddImageToBusiness(businessName, imageUrl, Image));
            }
        }

        [TestMethod]
        public async Task Throw_WhenFile_IsNotImage()
        {
            var dabataseName = nameof(Throw_WhenFile_IsNotImage);

            var options = BusinessTestUtil.GetOptions(dabataseName);

            BusinessTestUtil.FillContextWithBusinesses(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            string businessName = "Hilton";
            string imageUrl = "Hilton_picture.jpg";
            IFormFile Image = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new BusinessService(actAndAssertContext, mappingProviderMock.Object);

                await Assert.ThrowsExceptionAsync<EntityInvalidException>(
                                    async () => await sut.AddImageToBusiness(businessName, imageUrl, Image));
            }
        }

        [TestMethod]
        public async Task Throw_WhenBusiness_HasAlreadyLogo()
        {
            var dabataseName = nameof(Throw_WhenBusiness_HasAlreadyLogo);

            var options = BusinessTestUtil.GetOptions(dabataseName);

            BusinessTestUtil.FillContextWithBusinesses(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            string businessName = "GROS";
            string imageUrl = "GROS_logo.jpg";
            //  IFormFile Image = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");

            var currDir = Directory.GetCurrentDirectory();
            using (var stream = File.OpenRead(@"..\\..\\..\\ImagesUsedForTests\\GROS_logo.jpg"))
            {
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(@"/ImagesUsedForTests/GROS_logo.jpg"))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg"
                };

                using (var actAndAssertContext = new ApplicationDbContext(options))
                {
                    var sut = new BusinessService(actAndAssertContext, mappingProviderMock.Object);

                    await Assert.ThrowsExceptionAsync<EntityAlreadyExistsException>(
                                        async () => await sut.AddImageToBusiness(businessName, imageUrl, file));
                }
            };
        }

        [TestMethod]
        public async Task AddImage_WhenAllParametersAreValid()
        {
            var dabataseName = nameof(AddImage_WhenAllParametersAreValid);

            var options = BusinessTestUtil.GetOptions(dabataseName);

            BusinessTestUtil.FillContextWithBusinesses(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            string businessName = "Hilton";
            string imageUrl = "Hilton_logo.jpg";

            var currDir = Directory.GetCurrentDirectory();
            using (var stream = File.OpenRead(@"..\\..\\..\\ImagesUsedForTests\\Hilton_logo.jpg"))
            {
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(@"/ImagesUsedForTests/GROS_logo.jpg"))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg"
                };

                using (var actAndAssertContext = new ApplicationDbContext(options))
                {
                    var sut = new BusinessService(actAndAssertContext, mappingProviderMock.Object);

                    await sut.AddImageToBusiness(businessName, imageUrl, file);

                    var log = actAndAssertContext.Businesses.FirstOrDefault(b => b.Name == businessName);

                    Assert.IsTrue(log.Images.Count() == 1);
                    Assert.IsTrue(actAndAssertContext.Images.Any(m => m.Name == imageUrl));
                }
            };
        }

        [TestMethod]
        public async Task ReturnCorrectViewModel()
        {
            var dabataseName = nameof(ReturnCorrectViewModel);

            var options = BusinessTestUtil.GetOptions(dabataseName);

            BusinessTestUtil.FillContextWithBusinesses(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            mappingProviderMock.Setup(x => x.MapTo<BusinessViewModel>(It.IsAny<Business>())).Returns(new BusinessViewModel());

            string businessName = "Hilton";
            string imageUrl = "Hilton_logo.jpg";

            var currDir = Directory.GetCurrentDirectory();
            using (var stream = File.OpenRead(@"..\\..\\..\\ImagesUsedForTests\\Hilton_logo.jpg"))
            {
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(@"/ImagesUsedForTests/GROS_logo.jpg"))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg"
                };

                using (var actAndAssertContext = new ApplicationDbContext(options))
                {
                    var sut = new BusinessService(actAndAssertContext, mappingProviderMock.Object);

                    var result = await sut.AddImageToBusiness(businessName, imageUrl, file);

                    Assert.IsInstanceOfType(result, typeof(BusinessViewModel));
                }
            }
        }
    }
}