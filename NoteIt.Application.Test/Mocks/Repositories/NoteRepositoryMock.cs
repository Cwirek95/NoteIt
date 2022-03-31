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

            var note1 = new Note()
            {
                Id = 1,
                Name = "Note1",
                Content = "NoteContent1",
                IsHidden = false,
                IsImportant = false,
                CreatedAt = DateTimeOffset.Now.AddMinutes(-1),
                LastModified = DateTimeOffset.Now,
            };
            var note2 = new Note()
            {
                Id = 2,
                Name = "Note2",
                Content = "NoteContent2",
                IsHidden = false,
                IsImportant = false,
                CreatedAt = DateTimeOffset.Now.AddMinutes(-1),
                LastModified = DateTimeOffset.Now,
            };
            var note3 = new Note()
            {
                Id = 3,
                Name = "Note3",
                Content = "NoteContent3",
                IsHidden = false,
                IsImportant = false,
                CreatedAt = DateTimeOffset.Now.AddMinutes(-1),
                LastModified = DateTimeOffset.Now,
            };
            var note4 = new Note()
            {
                Id = 4,
                Name = "Note4",
                Content = "NoteContent4",
                IsHidden = false,
                IsImportant = false,
                CreatedAt = DateTimeOffset.Now.AddMinutes(-1),
                LastModified = DateTimeOffset.Now,
            };
            var note5 = new Note()
            {
                Id = 5,
                Name = "Note5",
                Content = "NoteContent5",
                IsHidden = false,
                IsImportant = false,
                CreatedAt = DateTimeOffset.Now.AddMinutes(-1),
                LastModified = DateTimeOffset.Now,
            };
            notes.Add(note1);
            notes.Add(note2);
            notes.Add(note3);
            notes.Add(note4);
            notes.Add(note5);

            return notes;
        }
    }
}
