using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.Infrastructure;
using HotelManagement.Services;
using HotelManagement.Services.Exceptions;
using HotelManagement.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.ServiceTests.CategoryServiceTests
{
    [TestClass]
    public class CreateCategoryAsync_Should
    {
        [TestMethod]
        public async Task Throw_WhenLogbook_DoesNotExist()
        {
            var dabataseName = nameof(Throw_WhenLogbook_DoesNotExist);

            var options = CategoryTestUtil.GetOptions(dabataseName);

            CategoryTestUtil.FillContextWithCategories(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new CategoryService(actAndAssertContext, mappingProviderMock.Object);
                string categoryName = "Health and Safety";
                string logbookName = "Cleaners";

                await Assert.ThrowsExceptionAsync<EntityInvalidException>(
                        async () => await sut.CreateCategoryAsync(categoryName, logbookName));
            }
        }

        [TestMethod]
        public async Task Throw_WhenCategory_DoesExists()
        {
            var dabataseName = nameof(Throw_WhenCategory_DoesExists);

            var options = CategoryTestUtil.GetOptions(dabataseName);

            CategoryTestUtil.FillContextWithCategories(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new CategoryService(actAndAssertContext, mappingProviderMock.Object);
                string categoryName = "Maintenance";
                string logbookName = "Swimming Pool";

                await Assert.ThrowsExceptionAsync<EntityAlreadyExistsException>(
                        async () => await sut.CreateCategoryAsync(categoryName, logbookName));
            }
        }

        [TestMethod]
        public async Task AddNewCategory_WhenAllParametersAreValid()
        {
            var dabataseName = nameof(AddNewCategory_WhenAllParametersAreValid);

            var options = CategoryTestUtil.GetOptions(dabataseName);

            CategoryTestUtil.FillContextWithCategories(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            var discoLogbook = new Logbook()
            {
                Name = "Disco",
                Description = "A new room for clubbing has opened",
                Id = "a0ab4621-a92c-4735-bcab-3e35eabae03d",
                Categories = new List<Category>()
            };

            string categoryName = "Staff";

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Logbooks.Add(discoLogbook);

                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new CategoryService(actAndAssertContext, mappingProviderMock.Object);

                await sut.CreateCategoryAsync(categoryName, discoLogbook.Name);

                var log = actAndAssertContext.Logbooks.FirstOrDefault(x => x.Name == discoLogbook.Name);

                Assert.IsTrue(log.Categories.Count() == 1);
                Assert.IsTrue(actAndAssertContext.Categories.Any(m => m.Name == categoryName));
            }
        }

        [TestMethod]
        public async Task ReturnCorrectViewModel()
        {
            var dabataseName = nameof(ReturnCorrectViewModel);

            var options = CategoryTestUtil.GetOptions(dabataseName);

            CategoryTestUtil.FillContextWithCategories(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            mappingProviderMock.Setup(x => x.MapTo<CategoryViewModel>(It.IsAny<Category>())).Returns(new CategoryViewModel());

            string categoryName = "Waiting staff";

            string logbookName = "Swimming Pool";

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new CategoryService(actAndAssertContext, mappingProviderMock.Object);

                var result = await sut.CreateCategoryAsync(categoryName, logbookName);

                Assert.IsInstanceOfType(result, typeof(CategoryViewModel));
            }
        }
    }
}