using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteIt.Application.Functions.Contacts.Commands.ChangeContactVisibility;
using NoteIt.Application.Functions.Contacts.Commands.CreateContact;
using NoteIt.Application.Functions.Contacts.Commands.DeleteContact;
using NoteIt.Application.Functions.Contacts.Commands.UpdateContact;
using NoteIt.Application.Functions.Contacts.Queries.GetContactDetail;
using NoteIt.Application.Functions.Contacts.Queries.GetContactsListByStorage;

namespace NoteIt.Api.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{address}/[controller]", Name = "GetAllContacts")]
        public async Task<ActionResult<ContactsInListByStorageViewModel>> GetAllByStorage(string address)
        {
            var getContactsByStorageId = new GetContactsListByStorageQuery()
            { 
                StorageAddress = address
            };
            var response = await _mediator.Send(getContactsByStorageId);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{address}/[controller]/{id}")]
        public async Task<ActionResult<ContactDetailViewModel>> Detail(int id, string address)
        {
            var getContactDetail = new GetContactDetailQuery()
            {
                Id = id,
                StorageAddress = address
            };
            var response = await _mediator.Send(getContactDetail);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost("{address}/[controller]")]
        public async Task<ActionResult<int>> Create(string address, [FromBody] CreateContactCommand command)
        {
            var createContactCommand = new CreateContactCommand()
            {
                Id = command.Id,
                Name = command.Name,
                EmailAddress = command.EmailAddress,
                PhoneNumber = command.PhoneNumber,
                StorageAddress = address
            };
            var response = await _mediator.Send(createContactCommand);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut("{address}/[controller]/{id}")]
        public async Task<ActionResult> Update(int id, string address, [FromBody] UpdateContactCommand command)
        {
            var updateContactCommand = new UpdateContactCommand()
            {
                Id = id,
                Name = command.Name,
                EmailAddress = command.EmailAddress,
                PhoneNumber = command.PhoneNumber,
                StorageAddress = address
            };
            await _mediator.Send(updateContactCommand);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut("{address}/[controller]/{id}/visibility", Name = "ChangeContactVisibility")]
        public async Task<ActionResult> UpdateVisibility(int id, string address)
        {
            var changeContactVisibiltyCommand = new ChangeContactVisibilityCommand()
            {
                Id = id,
                StorageAddress = address
            };
            await _mediator.Send(changeContactVisibiltyCommand);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpDelete("{address}/[controller]/{id}")]
        public async Task<ActionResult> Delete(int id, string address)
        {
            var deleteCommand = new DeleteContactCommand()
            {
                Id = id,
                StorageAddress = address
            };
            await _mediator.Send(deleteCommand);

            return NoContent();
        }
    }
}