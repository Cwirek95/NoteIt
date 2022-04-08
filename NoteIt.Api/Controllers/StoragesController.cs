using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteIt.Application.Functions.Storages.Commands.CreateStorage;
using NoteIt.Application.Functions.Storages.Commands.LogInStorage;
using NoteIt.Application.Security.Models;

namespace NoteIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("authenticate", Name = "LogIn")]
        public async Task<ActionResult<AuthenticationResponse>> LogIn([FromBody] LogInStorageCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

    }
}
