using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.Infrastructure;
using HotelManagement.Services;
using HotelManagement.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.ServiceTests.BusinessServiceTests
{
    [TestClass]
    public class GetBusinesses_Should
    {
        [TestMethod]
        public async Task Return_Businesses_OrderedByName_DescendingFalse()
        {
            var dabataseName = nameof(Return_Businesses_OrderedByName_DescendingFalse);

            var options = BusinessTestUtil.GetOptions(dabataseName);

            // We fill the context with data and save it.

            var mappingProviderMock = new Mock<IMappingProvider>();

            var collectionOfBusinesses = new List<Business>();

            mappingProviderMock
                .Setup(x => x.MapTo<ICollection<BusinessViewModel>>(It.IsAny<List<Business>>()))
                .Callback<object>(inputargs => collectionOfBusinesses = inputargs as List<Business>);

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Businesses.Add(new Business()
                {
                    Name = "BBusiness",
                    Description = "BDescription",
                    Location = "BLocation",
                    CreatedOn = DateTime.Parse("5/20/2019 2:40:05 PM")
                });
                arrangeContext.Businesses.Add(new Business()
                {
                    Name = "ABusiness",
                    Description = "ADescription",
                    Location = "ALocation",
                    CreatedOn = DateTime.Parse("5/25/2019 2:40:05 PM")
                });

                await arrangeContext.SaveChangesAsync();
            }

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new BusinessService(actAndAssertContext, mappingProviderMock.Object);
                string orderBy = "name";

                await sut.GetBusinesses(orderBy, false);

                Assert.AreEqual(2, collectionOfBusinesses.Count());
                Assert.AreEqual("ABusiness", collectionOfBusinesses.FirstOrDefault().Name);
            }
        }

        [TestMethod]
        public async Task Return_Businesses_OrderedByName_DescendingTrue()
        {
            var dabataseName = nameof(Return_Businesses_OrderedByName_DescendingTrue);

            var options = BusinessTestUtil.GetOptions(dabataseName);

            // We fill the context with data and save it.

            var mappingProviderMock = new Mock<IMappingProvider>();

            var collectionOfBusinesses = new List<Business>();

            mappingProviderMock
                .Setup(x => x.MapTo<ICollection<BusinessViewModel>>(It.IsAny<List<Business>>()))
                .Callback<object>(inputargs => collectionOfBusinesses = inputargs as List<Business>);

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Businesses.Add(new Business()
                {
                    Name = "BBusiness",
                    Description = "BDescription",
                    Location = "BLocation",
                    CreatedOn = DateTime.Parse("5/20/2019 2:40:05 PM")
                });
                arrangeContext.Businesses.Add(new Business()
                {
                    Name = "ABusiness",
                    Description = "ADescription",
                    Location = "ALocation",
                    CreatedOn = DateTime.Parse("5/25/2019 2:40:05 PM")
                });

                await arrangeContext.SaveChangesAsync();
            }

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new BusinessService(actAndAssertContext, mappingProviderMock.Object);
                string orderBy = "name";

                await sut.GetBusinesses(orderBy, true);

                Assert.AreEqual(2, collectionOfBusinesses.Count());
                Assert.AreEqual("BBusiness", collectionOfBusinesses.FirstOrDefault().Name);
            }
        }

        [TestMethod]
        public async Task Return_Businesses_OrderedByDate_DescendingTrue()
        {
            var dabataseName = nameof(Return_Businesses_OrderedByDate_DescendingTrue);

            var options = BusinessTestUtil.GetOptions(dabataseName);

            // We fill the context with data and save it.

            var mappingProviderMock = new Mock<IMappingProvider>();

            var collectionOfBusinesses = new List<Business>();

            mappingProviderMock
                .Setup(x => x.MapTo<ICollection<BusinessViewModel>>(It.IsAny<List<Business>>()))
                .Callback<object>(inputargs => collectionOfBusinesses = inputargs as List<Business>);

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Businesses.Add(new Business()
                {
                    Name = "BBusiness",
                    Description = "BDescription",
                    Location = "BLocation",
                    CreatedOn = DateTime.Parse("5/20/2019 2:40:05 PM")
                });
                arrangeContext.Businesses.Add(new Business()
                {
                    Name = "ABusiness",
                    Description = "ADescription",
                    Location = "ALocation",
                    CreatedOn = DateTime.Parse("5/25/2019 2:40:05 PM")
                });

                await arrangeContext.SaveChangesAsync();
            }

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new BusinessService(actAndAssertContext, mappingProviderMock.Object);
                string orderBy = "date";

                await sut.GetBusinesses(orderBy, true);

                Assert.AreEqual(2, collectionOfBusinesses.Count());
                Assert.AreEqual("BBusiness", collectionOfBusinesses.FirstOrDefault().Name);
            }
        }

        [TestMethod]
        public async Task Return_Businesses_OrderedByDate_DescendingFalse()
        {
            var dabataseName = nameof(Return_Businesses_OrderedByDate_DescendingFalse);

            var options = BusinessTestUtil.GetOptions(dabataseName);

            // We fill the context with data and save it.

            var mappingProviderMock = new Mock<IMappingProvider>();

            var collectionOfBusinesses = new List<Business>();

            mappingProviderMock
                .Setup(x => x.MapTo<ICollection<BusinessViewModel>>(It.IsAny<List<Business>>()))
                .Callback<object>(inputargs => collectionOfBusinesses = inputargs as List<Business>);

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Businesses.Add(new Business()
                {
                    Name = "BBusiness",
                    Description = "BDescription",
                    Location = "BLocation",
                    CreatedOn = DateTime.Parse("5/20/2019 2:40:05 PM")
                });
                arrangeContext.Businesses.Add(new Business()
                {
                    Name = "ABusiness",
                    Description = "ADescription",
                    Location = "ALocation",
                    CreatedOn = DateTime.Parse("5/25/2019 2:40:05 PM")
                });

                await arrangeContext.SaveChangesAsync();
            }

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new BusinessService(actAndAssertContext, mappingProviderMock.Object);
                string orderBy = "date";

                await sut.GetBusinesses(orderBy, false);

                Assert.AreEqual(2, collectionOfBusinesses.Count());
                Assert.AreEqual("ABusiness", collectionOfBusinesses.FirstOrDefault().Name);
            }
        }

        [TestMethod]
        public async Task Call_MapperFunction_Once()
        {
            var dabataseName = nameof(Call_MapperFunction_Once);

            var options = BusinessTestUtil.GetOptions(dabataseName);

            // We fill the context with data and save it.

            var mappingProviderMock = new Mock<IMappingProvider>();

            var collectionOfBusinesses = new List<Business>();

            mappingProviderMock
                .Setup(x => x.MapTo<ICollection<BusinessViewModel>>(It.IsAny<List<Business>>()))
                .Callback<object>(inputargs => collectionOfBusinesses = inputargs as List<Business>);

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new BusinessService(actAndAssertContext, mappingProviderMock.Object);
                string orderBy = "date";

                await sut.GetBusinesses(orderBy, false);

                mappingProviderMock.Verify(m => m.MapTo<ICollection<BusinessViewModel>>(collectionOfBusinesses), Times.Once);
            }
        }
    }
}