using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NoteIt.Application.Contracts.Repositories;
using NoteIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoteIt.Application.Test.Mocks.Repositories
{
    public class NoteRepositoryMock
    {
        public static Mock<INoteRepository> GetNoteRepository()
        {
            var notes = GetNotes();
            var mockNoteRepository = new Mock<INoteRepository>();

            mockNoteRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(notes);
            mockNoteRepository.Setup(x => x.GetAllAsyncByStorageId(It.IsAny<Guid>())).ReturnsAsync(
                (Guid id) =>
                {
                    var notesList = notes.Where(x => x.StorageId == id).ToList();
                    return notesList;
                });
            mockNoteRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    var note = notes.FirstOrDefault(x => x.Id == id);
                    return note;
                });
            mockNoteRepository.Setup(x => x.AddAsync(It.IsAny<Note>())).ReturnsAsync(
                (Note note) =>
                {
                    notes.Add(note);
                    return note;
                });
            mockNoteRepository.Setup(x => x.UpdateAsync(It.IsAny<Note>())).Callback<Note>(
                (note) =>
                {
                    notes.RemoveAll(x => x.Id == note.Id);
                    notes.Add(note);
                });
            mockNoteRepository.Setup(x => x.DeleteAsync(It.IsAny<Note>())).Callback<Note>(
                (note) =>
                {
                    notes.Remove(note);
                });

            return mockNoteRepository;
        }

        private static List<Note> GetNotes()
        {
            List<Note> notes = new List<Note>();
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var note1 = fixture.Create<Note>();
            var note2 = fixture.Create<Note>();
            var note3 = fixture.Create<Note>();
            var note4 = fixture.Create<Note>();
            var note5 = fixture.Create<Note>();

            notes.Add(note1);
            notes.Add(note2);
            notes.Add(note3);
            notes.Add(note4);
            notes.Add(note5);

            return notes;
        }
    }
}
