using MediatR;

namespace NoteIt.Application.Functions.Notes.Queries.GetNoteDetail
{
    public class GetNoteDetailQuery : IRequest<NoteDetailViewModel>
    {
        public int Id { get; set; }


        public string StorageAddress { get; set; }
    }
}
