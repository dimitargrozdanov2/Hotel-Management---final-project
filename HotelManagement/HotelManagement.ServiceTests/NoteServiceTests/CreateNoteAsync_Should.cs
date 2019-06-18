using HotelManagement.Data;
using HotelManagement.DataModels;
using HotelManagement.Infrastructure;
using HotelManagement.Services;
using HotelManagement.ViewModels;
using HotelManagement.ViewModels.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.ServiceTests.NoteServiceTests
{
    [TestClass]
    public class CreateNoteAsync_Should
    {
        [TestMethod]
        public async Task Throw_When_LogbookDoesNotExist()
        {
            var databaseName = nameof(Throw_When_LogbookDoesNotExist);

            var options = NoteTestUtils.GetOptions(databaseName);

            var mappingProviderMock = new Mock<IMappingProvider>();

            var parameterModel = new CreateNoteViewModel();

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new NoteService(actAndAssertContext, mappingProviderMock.Object);

                await Assert.ThrowsExceptionAsync<ArgumentException>(
                    async () => await sut.CreateNoteAsync(parameterModel));
            }
        }

        [TestMethod]
        public async Task Throw_When_CategoryDoesNotExist()
        {
            var databaseName = nameof(Throw_When_CategoryDoesNotExist);

            var options = NoteTestUtils.GetOptions(databaseName);

            NoteTestUtils.FillContextWithUserData(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            var parameterModel = new CreateNoteViewModel();
            parameterModel.Logbook = "Restaurant";
            parameterModel.Category = "Invalid";
            parameterModel.Email = "admin@admin.admin";

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new NoteService(actAndAssertContext, mappingProviderMock.Object);

                await Assert.ThrowsExceptionAsync<ArgumentException>(
                    async () => await sut.CreateNoteAsync(parameterModel));
            }
        }

        [TestMethod]
        public async Task Create_Note_Successfully()
        {
            var databaseName = nameof(Create_Note_Successfully);

            var options = NoteTestUtils.GetOptions(databaseName);

            NoteTestUtils.FillContextWithUserData(options);

            var mappingProviderMock = new Mock<IMappingProvider>();

            var parameterModel = new CreateNoteViewModel();

            parameterModel.Text = "This is a new note";
            parameterModel.Category = "TODO";
            parameterModel.Priority = "Low";
            parameterModel.Email = "admin@admin.admin";
            parameterModel.Logbook = "Restaurant";

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                Assert.IsTrue(actAndAssertContext.Notes.Count() == 0);
                var sut = new NoteService(actAndAssertContext, mappingProviderMock.Object);

                await sut.CreateNoteAsync(parameterModel);
                Assert.IsTrue(actAndAssertContext.Notes.Count() == 1);
            }
        }

        [TestMethod]
        public async Task Call_MappingFunction_Once()
        {
            var databaseName = nameof(Call_MappingFunction_Once);

            var options = NoteTestUtils.GetOptions(databaseName);

            NoteTestUtils.FillContextWithUserData(options);

            Note note = null;

            var mappingProviderMock = new Mock<IMappingProvider>();
            mappingProviderMock
                .Setup(x => x.MapTo<NoteViewModel>(It.IsAny<Note>()))
                .Callback<object>(inputargs => note = inputargs as Note);

            var parameterModel = new CreateNoteViewModel();

            parameterModel.Text = "This is a new note";
            parameterModel.Category = "TODO";
            parameterModel.Priority = "Low";
            parameterModel.Email = "admin@admin.admin";
            parameterModel.Logbook = "Restaurant";

            using (var actAndAssertContext = new ApplicationDbContext(options))
            {
                var sut = new NoteService(actAndAssertContext, mappingProviderMock.Object);

                await sut.CreateNoteAsync(parameterModel);
                mappingProviderMock.Verify(m => m.MapTo<NoteViewModel>(note), Times.Once);
            }
        }
    }
}