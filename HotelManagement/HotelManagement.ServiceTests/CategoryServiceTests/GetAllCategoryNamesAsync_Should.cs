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
    public class GetAllCategoryNamesAsync_Should
    {
        [TestMethod]
        public async Task Throw_When_LogbookDoesNotExist()
        {
            var databaseName = nameof(Throw_When_LogbookDoesNotExist);

            var options = CategoryTestUtil.GetOptions(databaseName);

            var mappingProviderMock = new Mock<IMappingProvider>();

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new CategoryService(actAndAssertContext, mappingProviderMock.Object);
                string logbookName = "InvalidLogbook";

                await Assert.ThrowsExceptionAsync<EntityInvalidException>(
                    async () => await sut.GetAllCategoryNamesAsync(logbookName));
            }
        }

        [TestMethod]
        public async Task Throw_When_Logbook_HasNoCategories()
        {
            var databaseName = nameof(Throw_When_Logbook_HasNoCategories);

            var options = CategoryTestUtil.GetOptions(databaseName);

            var mappingProviderMock = new Mock<IMappingProvider>();

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Logbooks.Add(new Logbook()
                {
                    Name = "Restaurant",
                    Description = "Beautiful and delicious place to have a dinner at!",
                    Id = "5aa43e5a-d189-4bcf-8c85-010280979fd0"
                });
            }

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new CategoryService(actAndAssertContext, mappingProviderMock.Object);
                string logbookName = "Restaurant";

                await Assert.ThrowsExceptionAsync<EntityInvalidException>(
                    async () => await sut.GetAllCategoryNamesAsync(logbookName));
            }
        }

        [TestMethod]
        public async Task Return_CategoryName_Successfully()
        {
            var databaseName = nameof(Return_CategoryName_Successfully);

            var options = CategoryTestUtil.GetOptions(databaseName);

            CategoryTestUtil.FillContextWithCategories(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new CategoryService(actAndAssertContext, mappingProviderMock.Object);
                string logbookName = "Swimming Pool";

                var returnCategories = await sut.GetAllCategoryNamesAsync(logbookName);

                Assert.AreEqual(1, returnCategories.Count);
                Assert.IsTrue(returnCategories.Contains("Maintenance"));
            }
        }
    }
}
