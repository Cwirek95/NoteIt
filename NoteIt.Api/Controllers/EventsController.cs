using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteIt.Application.Functions.Events.Commands.ChangeEventVisibility;
using NoteIt.Application.Functions.Events.Commands.CreateEvent;
using NoteIt.Application.Functions.Events.Commands.DeleteEvent;
using NoteIt.Application.Functions.Events.Commands.UpdateEvent;
using NoteIt.Application.Functions.Events.Queries.GetEventDetail;
using NoteIt.Application.Functions.Events.Queries.GetEventsListByStorage;

namespace NoteIt.Api.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{address}/[controller]", Name = "GetAllEvents")]
        public async Task<ActionResult<EventsInListByStorageViewModel>> GetAllByStorage(string address)
        {
            var getEventsByStorageId = new GetEventsListByStorageQuery()
            { 
                StorageAddress = address
            };
            var response = await _mediator.Send(getEventsByStorageId);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{address}/[controller]/{id}")]
        public async Task<ActionResult<EventDetailViewModel>> Detail(int id, string address)
        {
            var getEventDetail = new GetEventDetailQuery() 
            { 
                Id = id, 
                StorageAddress = address
            };
            var response = await _mediator.Send(getEventDetail);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost("{address}/[controller]")]
        public async Task<ActionResult<int>> Create(string address, [FromBody] CreateEventCommand command)
        {
            var createEventCommand = new CreateEventCommand()
            {
                Id = command.Id,
                Name = command.Name,
                Location = command.Location,
                Description = command.Description,
                StartDate = command.StartDate,
                EndDate = command.EndDate,
                ReminderDate = command.ReminderDate,
                StorageAddress = address
            };
            var response = await _mediator.Send(createEventCommand);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut("{address}/[controller]/{id}")]
        public async Task<ActionResult> Update(int id, string address, [FromBody] UpdateEventCommand command)
        {
            var updateEventCommand = new UpdateEventCommand()
            {
                Id = id,
                Name = command.Name,
                Location = command.Location,
                Description = command.Description,
                StartDate = command.StartDate,
                EndDate = command.EndDate,
                ReminderDate = command.ReminderDate,
                StorageAddress = address
            };
            await _mediator.Send(updateEventCommand);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut("{address}/[controller]/{id}/visibility", Name = "ChangeEventVisibility")]
        public async Task<ActionResult> UpdateVisibility(int id, string address)
        {
            var changeEventVisibilityCommand = new ChangeEventVisibilityCommand()
            {
                Id = id,
                StorageAddress = address
            };
            await _mediator.Send(changeEventVisibilityCommand);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpDelete("{address}/[controller]/{id}")]
        public async Task<ActionResult> Delete(int id, string address)
        {
            var deleteCommand = new DeleteEventCommand()
            {
                Id = id,
                StorageAddress = address
            };
            await _mediator.Send(deleteCommand);

            return NoContent();
        }
    }
}
