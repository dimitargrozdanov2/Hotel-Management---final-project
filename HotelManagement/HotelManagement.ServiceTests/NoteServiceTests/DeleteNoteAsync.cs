using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.DataModels.Enums;
using HotelManagement.Infrastructure;
using HotelManagement.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.ServiceTests.NoteServiceTests
{
    [TestClass]
    public class DeleteNoteAsync
    {
        [TestMethod]
        public async Task Throw_When_NoteDoesNotExist()
        {
            var databaseName = nameof(Throw_When_NoteDoesNotExist);

            var options = NoteTestUtils.GetOptions(databaseName);

            var mappingProviderMock = new Mock<IMappingProvider>();

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new NoteService(actAndAssertContext, mappingProviderMock.Object);

                await Assert.ThrowsExceptionAsync<ArgumentException>(
                    async () => await sut.DeleteNoteAsync("Invalid"));
            }
        }

        [TestMethod]
        public async Task Delete_Note_Successfully()
        {
            var databaseName = nameof(Delete_Note_Successfully);

            var options = NoteTestUtils.GetOptions(databaseName);

            NoteTestUtils.FillContextWithUserData(options);

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                var note = new Note()
                {
                    Id = "c74af441-a8d3-4002-b5cb-4aca8e9e157d",
                    Text = "New Note",
                    CategoryId = "814bd455-6873-4eb4-a6d1-b2b368539720",
                    PriorityType = (PriorityType)1,
                    UserId = "6536bb9a-3af0-40fe-a878-e5ab8212cd55",
                    LogbookId = "fef91cb6-32ed-491d-9e69-81acc2bbe523"
                };

                arrangeContext.Notes.Add(note);
                await arrangeContext.SaveChangesAsync();
            }

            var mappingProviderMock = new Mock<IMappingProvider>();

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new NoteService(actAndAssertContext, mappingProviderMock.Object);

                await sut.DeleteNoteAsync("c74af441-a8d3-4002-b5cb-4aca8e9e157d");

                Assert.IsTrue(actAndAssertContext.Notes.Count() == 0);
            }
        }

        [TestMethod]
        public async Task Return_Deleted_Note_Id()
        {
            var databaseName = nameof(Delete_Note_Successfully);

            var options = NoteTestUtils.GetOptions(databaseName);

            NoteTestUtils.FillContextWithUserData(options);

            var noteId = "c74af441-a8d3-4002-b5cb-4aca8e9e157d";

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                var note = new Note()
                {
                    Id = noteId,
                    Text = "New Note",
                    CategoryId = "814bd455-6873-4eb4-a6d1-b2b368539720",
                    PriorityType = (PriorityType)1,
                    UserId = "6536bb9a-3af0-40fe-a878-e5ab8212cd55",
                    LogbookId = "fef91cb6-32ed-491d-9e69-81acc2bbe523"
                };

                arrangeContext.Notes.Add(note);
                await arrangeContext.SaveChangesAsync();
            }

            var mappingProviderMock = new Mock<IMappingProvider>();

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new NoteService(actAndAssertContext, mappingProviderMock.Object);

                var returnedNoteId = await sut.DeleteNoteAsync(noteId);

                Assert.AreEqual(returnedNoteId, noteId);
            }
        }
    }
}
