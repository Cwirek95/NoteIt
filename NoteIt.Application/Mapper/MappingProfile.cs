using AutoMapper;
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
        }
    }
}
