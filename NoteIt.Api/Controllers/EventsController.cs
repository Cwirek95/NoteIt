using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoteIt.Application.Functions.Events.Commands.ChangeEventVisibility;
using NoteIt.Application.Functions.Events.Commands.CreateEvent;
using NoteIt.Application.Functions.Events.Commands.DeleteEvent;
using NoteIt.Application.Functions.Events.Commands.UpdateEvent;
using NoteIt.Application.Functions.Events.Queries.GetEventDetail;
using NoteIt.Application.Functions.Events.Queries.GetEventsListByStorage;

namespace NoteIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("all/{storageId}", Name = "GetAllEvents")]
        public async Task<ActionResult<EventsInListByStorageViewModel>> GetAllByStorageId(Guid storageId)
        {
            var getEventsByStorageId = new GetEventsListByStorageQuery() { StorageId = storageId };
            var response = await _mediator.Send(getEventsByStorageId);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<EventDetailViewModel>> Detail(int id)
        {
            var getEventDetail = new GetEventDetailQuery() { Id = id };
            var response = await _mediator.Send(getEventDetail);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateEventCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateEventCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut("visibility", Name = "ChangeEventVisibility")]
        public async Task<ActionResult> UpdateVisibility([FromBody] ChangeEventVisibilityCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] DeleteEventCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
