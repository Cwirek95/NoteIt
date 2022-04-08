using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("{address}/[controller]", Name = "GetAllNotes")]
        public async Task<ActionResult<NotesInListByStorageViewModel>> GetAllByStorage(string address)
        {
            var getNotesByStorageId = new GetNotesListByStorageQuery()
            { 
                StorageAddress = address
            };
            var response = await _mediator.Send(getNotesByStorageId);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("{address}/[controller]/{id}")]
        public async Task<ActionResult<NoteDetailViewModel>> Detail(int id, string address)
        {
            var getNoteDetail = new GetNoteDetailQuery()
            {
                Id = id, 
                StorageAddress = address
            };
            var response = await _mediator.Send(getNoteDetail);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost("{address}/[controller]")]
        public async Task<ActionResult<int>> Create(string address, [FromBody] CreateNoteCommand command)
        {
            var createNoteCommand = new CreateNoteCommand()
            {
                Id = command.Id,
                Name = command.Name,
                Content = command.Content,
                IsImportant = command.IsImportant,
                StorageAddress = address
            };
            var response = await _mediator.Send(createNoteCommand);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("{address}/[controller]/{id}")]
        public async Task<ActionResult> Update(int id, string address, [FromBody] UpdateNoteCommand command)
        {
            var updateNoteCommand = new UpdateNoteCommand()
            {
                Id = id,
                Name = command.Name,
                Content = command.Content,
                IsImportant = command.IsImportant,
                StorageAddress = address
            };
            await _mediator.Send(updateNoteCommand);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("{address}/[controller]/{id}/importance", Name = "ChangeNoteImportance")]
        public async Task<ActionResult> UpdateImportance(int id, string address)
        {
            var changeNoteImportanceCommand = new ChangeNoteImportanceCommand()
            {
                Id = id,
                StorageAddress = address
            };
            await _mediator.Send(changeNoteImportanceCommand);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("{address}/[controller]/{id}/visibility", Name = "ChangeNoteVisibility")]
        public async Task<ActionResult> UpdateVisibility(int id, string address)
        {
            var changeNoteVisibilityCommand = new ChangeNoteVisibilityCommand()
            {
                Id = id,
                StorageAddress = address
            };
            await _mediator.Send(changeNoteVisibilityCommand);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpDelete("{address}/[controller]/{id}")]
        public async Task<ActionResult> Delete(int id, string address)
        {
            var deleteCommand = new DeleteNoteCommand() 
            {
                Id = id,
                StorageAddress= address
            };
            await _mediator.Send(deleteCommand);

            return NoContent();
        }
    }
}
