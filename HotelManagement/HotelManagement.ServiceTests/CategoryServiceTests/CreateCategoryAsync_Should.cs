using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.Infrastructure;
using HotelManagement.Services;
using HotelManagement.Services.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            //string categoryName = "Health and Safety";
            //var category = new Category
            //{
            //    Name = "InProgress",
            //};
            //var category = new Mock<Category>();
            //var listofCategories = new List<Category> { category };

            //var logbook = new Logbook
            //{
            //    Name = "Cleaners",
            //    Description = "Enthusiastic team",
            //    Id = "a0ab4621-a92c-4735-bcab-3e35eabae03d",
            //    Categories = listofCategories
            //};
            string categoryName = "Health";

            string logbookName = "ToDo";

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                var sut = new CategoryService(arrangeContext, mappingProviderMock.Object);

                await sut.CreateCategoryAsync(categoryName, logbookName);

                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                Assert.IsTrue(actAndAssertContext.Categories.Count() == 1);
                Assert.IsTrue(actAndAssertContext.Categories.Any(m => m.Name == categoryName));
            }
        }

        [TestMethod]
        public async Task ReturnCorrectViewModel()
        {
        }
    }
}
