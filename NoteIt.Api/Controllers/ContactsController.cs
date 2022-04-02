using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoteIt.Application.Functions.Contacts.Commands.ChangeContactVisibility;
using NoteIt.Application.Functions.Contacts.Commands.CreateContact;
using NoteIt.Application.Functions.Contacts.Commands.DeleteContact;
using NoteIt.Application.Functions.Contacts.Commands.UpdateContact;
using NoteIt.Application.Functions.Contacts.Queries.GetContactDetail;
using NoteIt.Application.Functions.Contacts.Queries.GetContactsListByStorage;

namespace NoteIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("all/{storageId}", Name = "GetAllContacts")]
        public async Task<ActionResult<ContactsInListByStorageViewModel>> GetAllByStorageId(Guid storageId)
        {
            var getContactsByStorageId = new GetContactsListByStorageQuery() { StorageId = storageId };
            var response = await _mediator.Send(getContactsByStorageId);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDetailViewModel>> Detail(int id)
        {
            var getContactDetail = new GetContactDetailQuery() { Id = id };
            var response = await _mediator.Send(getContactDetail);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateContactCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateContactCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut("visibility", Name = "ChangeContactVisibility")]
        public async Task<ActionResult> UpdateVisibility([FromBody] ChangeContactVisibilityCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] DeleteContactCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }
    }
}