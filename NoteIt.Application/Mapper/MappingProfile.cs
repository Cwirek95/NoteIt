﻿using AutoMapper;
using NoteIt.Application.Functions.Contacts.Commands.CreateContact;
using NoteIt.Application.Functions.Contacts.Queries.GetContactDetail;
using NoteIt.Application.Functions.Contacts.Queries.GetContactsListByStorage;
using NoteIt.Application.Functions.Events.Commands.CreateEvent;
using NoteIt.Application.Functions.Events.Queries.GetEventDetail;
using NoteIt.Application.Functions.Events.Queries.GetEventsListByStorage;
using NoteIt.Application.Functions.Notes.Commands.CreateNote;
using NoteIt.Application.Functions.Notes.Queries.GetNoteDetail;
using NoteIt.Application.Functions.Notes.Queries.GetNotesListByStorage;
using NoteIt.Application.Functions.Storages.Commands.CreateStorage;
using NoteIt.Domain.Entities;

namespace NoteIt.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Storage
            CreateMap<Storage, CreateStorageCommand>().ReverseMap();
            #endregion

            #region Note
            CreateMap<CreateNoteCommand, Note>();
            CreateMap<Note, CreateNoteCommand>()
                .ForMember(x => x.StorageAddress, opt => opt.Ignore());
            CreateMap<Note, NotesInListByStorageViewModel>().ReverseMap();
            CreateMap<Note, NoteDetailViewModel>().ReverseMap();
            #endregion

            #region Event
            CreateMap<CreateEventCommand, Event>();
            CreateMap<Event, CreateEventCommand>().
                ForMember(x => x.StorageAddress, opt => opt.Ignore());
            CreateMap<Event, EventsInListByStorageViewModel>().ReverseMap();
            CreateMap<Event, EventDetailViewModel>().ReverseMap();
            #endregion

            #region Contact
            CreateMap<CreateContactCommand, Contact>();
            CreateMap<Contact, CreateContactCommand>()
                .ForMember(x => x.StorageAddress, opt => opt.Ignore());
            CreateMap<Contact, ContactsInListByStorageViewModel>().ReverseMap();
            CreateMap<Contact, ContactDetailViewModel>().ReverseMap();
            #endregion
        }
    }
}
