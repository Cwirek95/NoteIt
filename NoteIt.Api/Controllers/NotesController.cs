using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoteIt.Application.Functions.Notes.Commands.ChangeNoteImportance;
using NoteIt.Application.Functions.Notes.Commands.ChangeNoteVisibility;
using NoteIt.Application.Functions.Notes.Commands.CreateNote;
using NoteIt.Application.Functions.Notes.Commands.DeleteNote;
using NoteIt.Application.Functions.Notes.Commands.UpdateNote;
using NoteIt.Application.Functions.Notes.Queries.GetNoteDetail;
using NoteIt.Application.Functions.Notes.Queries.GetNotesListByStorage;

namespace NoteIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("all/{storageId}", Name = "GetAllNotes")]
        public async Task<ActionResult<NotesInListByStorageViewModel>> GetAllByStorageId(Guid storageId)
        {
            var getNotesByStorageId = new GetNoteListByStorageQuery() { StorageId = storageId };
            var response = await _mediator.Send(getNotesByStorageId);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDetailViewModel>> Detail(int id)
        {
            var getNoteDetail = new GetNoteDetailQuery() { Id = id };
            var response = await _mediator.Send(getNoteDetail);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateNoteCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateNoteCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut("importance", Name = "ChangeNoteImportance")]
        public async Task<ActionResult> UpdateImportance([FromBody] ChangeNoteImportanceCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut("visibility", Name = "ChangeNoteVisibility")]
        public async Task<ActionResult> UpdateVisibility([FromBody] ChangeNoteVisibilityCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] DeleteNoteCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
