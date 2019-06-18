using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.DataModels.Enums;
using HotelManagement.Infrastructure;
using HotelManagement.Services;
using HotelManagement.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.ServiceTests.NoteServiceTests
{
    [TestClass]
    public class SearchNotesAsync_Should
    {
        [TestMethod]
        public async Task SearchBy_Text_AndReturnOneNote()
        {
            var databaseName = nameof(SearchBy_Text_AndReturnOneNote);

            var options = NoteTestUtils.GetOptions(databaseName);

            NoteTestUtils.FillContextWithUserData(options);

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                var note = new Note()
                {
                    Id = "c74af441-a8d3-4002-b5cb-4aca8e9e157d",
                    Text = "New Note",
                    CategoryId = "b3905927-12d6-4227-b6a2-2c0677ce6cae",
                    PriorityType = (PriorityType)1,
                    UserId = "6536bb9a-3af0-40fe-a878-e5ab8212cd55",
                    LogbookId = "fef91cb6-32ed-491d-9e69-81acc2bbe523",
                    CreatedOn = DateTime.Parse("5/20/2019 2:40:05 PM")
                };

                var secondNote = new Note()
                {
                    Id = "1d3e7b65-178d-4e81-8e5a-421257238a45",
                    Text = "Basic",
                    CategoryId = "29eab335-a7d3-42b2-be09-6214d387a4da",
                    PriorityType = (PriorityType)1,
                    UserId = "6536bb9a-3af0-40fe-a878-e5ab8212cd55",
                    LogbookId = "fef91cb6-32ed-491d-9e69-81acc2bbe523",
                    CreatedOn = DateTime.Parse("5/25/2019 2:40:05 PM")
                };

                arrangeContext.Notes.Add(note);
                arrangeContext.Notes.Add(secondNote);
                await arrangeContext.SaveChangesAsync();
            }

            var mappingProviderMock = new Mock<IMappingProvider>();

            var collectionOfNotes = new List<Note>();

            mappingProviderMock
                .Setup(x => x.MapTo<ICollection<NoteViewModel>>(It.IsAny<List<Note>>()))
                .Callback<object>(inputargs => collectionOfNotes = inputargs as List<Note>);

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new NoteService(actAndAssertContext, mappingProviderMock.Object);

                var searchedText = "Basic";
                var userEmail = "admin@admin.admin";
                var searchByValue = "Text";

                await sut.SearchNotesAsync(searchedText, userEmail, searchByValue);

                Assert.IsTrue(collectionOfNotes.Count() == 1);
                Assert.IsTrue(collectionOfNotes.First().Text.Contains(searchedText));
            }
        }

        [TestMethod]
        public async Task SearchBy_Date_AndReturnOneNote()
        {
            var databaseName = nameof(SearchBy_Date_AndReturnOneNote);

            var options = NoteTestUtils.GetOptions(databaseName);

            NoteTestUtils.FillContextWithUserData(options);

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                var note = new Note()
                {
                    Id = "c74af441-a8d3-4002-b5cb-4aca8e9e157d",
                    Text = "New Note",
                    CategoryId = "b3905927-12d6-4227-b6a2-2c0677ce6cae",
                    PriorityType = (PriorityType)1,
                    UserId = "6536bb9a-3af0-40fe-a878-e5ab8212cd55",
                    LogbookId = "fef91cb6-32ed-491d-9e69-81acc2bbe523",
                    CreatedOn = DateTime.Parse("5/20/2019 2:40:05 PM")
                };

                var secondNote = new Note()
                {
                    Id = "1d3e7b65-178d-4e81-8e5a-421257238a45",
                    Text = "Basic",
                    CategoryId = "29eab335-a7d3-42b2-be09-6214d387a4da",
                    PriorityType = (PriorityType)1,
                    UserId = "6536bb9a-3af0-40fe-a878-e5ab8212cd55",
                    LogbookId = "fef91cb6-32ed-491d-9e69-81acc2bbe523",
                    CreatedOn = DateTime.Parse("5/25/2019 2:40:05 PM")
                };

                arrangeContext.Notes.Add(note);
                arrangeContext.Notes.Add(secondNote);
                await arrangeContext.SaveChangesAsync();
            }

            var mappingProviderMock = new Mock<IMappingProvider>();

            var collectionOfNotes = new List<Note>();

            mappingProviderMock
                .Setup(x => x.MapTo<ICollection<NoteViewModel>>(It.IsAny<List<Note>>()))
                .Callback<object>(inputargs => collectionOfNotes = inputargs as List<Note>);

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new NoteService(actAndAssertContext, mappingProviderMock.Object);

                var searchedText = "5/20/2019";
                var userEmail = "admin@admin.admin";
                var searchByValue = "Date Created";

                await sut.SearchNotesAsync(searchedText, userEmail, searchByValue);

                Assert.IsTrue(collectionOfNotes.Count() == 1);
                Assert.IsTrue(collectionOfNotes.First().Text.Contains("New Note"));
            }
        }

        [TestMethod]
        public async Task SearchBy_Category_AndReturnOneNote()
        {
            var databaseName = nameof(SearchBy_Category_AndReturnOneNote);

            var options = NoteTestUtils.GetOptions(databaseName);

            NoteTestUtils.FillContextWithUserData(options);

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                var note = new Note()
                {
                    Id = "c74af441-a8d3-4002-b5cb-4aca8e9e157d",
                    Text = "New Note",
                    CategoryId = "b3905927-12d6-4227-b6a2-2c0677ce6cae",
                    PriorityType = (PriorityType)1,
                    UserId = "6536bb9a-3af0-40fe-a878-e5ab8212cd55",
                    LogbookId = "fef91cb6-32ed-491d-9e69-81acc2bbe523",
                    CreatedOn = DateTime.Parse("5/20/2019 2:40:05 PM")
                };

                var secondNote = new Note()
                {
                    Id = "1d3e7b65-178d-4e81-8e5a-421257238a45",
                    Text = "Basic",
                    CategoryId = "29eab335-a7d3-42b2-be09-6214d387a4da",
                    PriorityType = (PriorityType)1,
                    UserId = "6536bb9a-3af0-40fe-a878-e5ab8212cd55",
                    LogbookId = "fef91cb6-32ed-491d-9e69-81acc2bbe523",
                    CreatedOn = DateTime.Parse("5/25/2019 2:40:05 PM")
                };

                arrangeContext.Notes.Add(note);
                arrangeContext.Notes.Add(secondNote);
                await arrangeContext.SaveChangesAsync();
            }

            var mappingProviderMock = new Mock<IMappingProvider>();

            var collectionOfNotes = new List<Note>();

            mappingProviderMock
                .Setup(x => x.MapTo<ICollection<NoteViewModel>>(It.IsAny<List<Note>>()))
                .Callback<object>(inputargs => collectionOfNotes = inputargs as List<Note>);

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new NoteService(actAndAssertContext, mappingProviderMock.Object);

                var searchedText = "TODO";
                var userEmail = "admin@admin.admin";
                var searchByValue = "Category";

                await sut.SearchNotesAsync(searchedText, userEmail, searchByValue);

                Assert.IsTrue(collectionOfNotes.Count() == 1);
                Assert.IsTrue(collectionOfNotes.First().Text.Contains("New Note"));
            }
        }

        [TestMethod]
        public async Task Call_MappingFunction_Once()
        {
            var databaseName = nameof(SearchBy_Category_AndReturnOneNote);

            var options = NoteTestUtils.GetOptions(databaseName);

            NoteTestUtils.FillContextWithUserData(options);

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                var note = new Note()
                {
                    Id = "c74af441-a8d3-4002-b5cb-4aca8e9e157d",
                    Text = "New Note",
                    CategoryId = "b3905927-12d6-4227-b6a2-2c0677ce6cae",
                    PriorityType = (PriorityType)1,
                    UserId = "6536bb9a-3af0-40fe-a878-e5ab8212cd55",
                    LogbookId = "fef91cb6-32ed-491d-9e69-81acc2bbe523",
                    CreatedOn = DateTime.Parse("5/20/2019 2:40:05 PM")
                };

                var secondNote = new Note()
                {
                    Id = "1d3e7b65-178d-4e81-8e5a-421257238a45",
                    Text = "Basic",
                    CategoryId = "29eab335-a7d3-42b2-be09-6214d387a4da",
                    PriorityType = (PriorityType)1,
                    UserId = "6536bb9a-3af0-40fe-a878-e5ab8212cd55",
                    LogbookId = "fef91cb6-32ed-491d-9e69-81acc2bbe523",
                    CreatedOn = DateTime.Parse("5/25/2019 2:40:05 PM")
                };

                arrangeContext.Notes.Add(note);
                arrangeContext.Notes.Add(secondNote);
                await arrangeContext.SaveChangesAsync();
            }

            var mappingProviderMock = new Mock<IMappingProvider>();

            var collectionOfNotes = new List<Note>();

            mappingProviderMock
                .Setup(x => x.MapTo<ICollection<NoteViewModel>>(It.IsAny<List<Note>>()))
                .Callback<object>(inputargs => collectionOfNotes = inputargs as List<Note>);

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new NoteService(actAndAssertContext, mappingProviderMock.Object);

                var searchedText = "TODO";
                var userEmail = "admin@admin.admin";
                var searchByValue = "Category";

                await sut.SearchNotesAsync(searchedText, userEmail, searchByValue);

                mappingProviderMock.Verify(m => m.MapTo<ICollection<NoteViewModel>>(collectionOfNotes), Times.Once);
            }
        }
    }
}