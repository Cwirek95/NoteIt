using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoteIt.Application.Functions.Storages.Commands.CreateStorage;

namespace NoteIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoragesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StoragesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateStorageCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

    }
}
