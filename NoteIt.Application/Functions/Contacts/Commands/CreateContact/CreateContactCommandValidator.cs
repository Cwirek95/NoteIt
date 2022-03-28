using FluentValidation;

namespace NoteIt.Application.Functions.Contacts.Commands.CreateContact
{
    public class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
    {
        public CreateContactCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(256)
                .WithMessage("{PropertyName} must not exceed 256 characters");

            RuleFor(x => x.EmailAddress)
                .EmailAddress()
                .WithMessage("Invalid {PropertyName} format")
                .MaximumLength(128)
                .WithMessage("{PropertyName} must not exceed 128 characters");

            RuleFor(x => x.PhoneNumber)
                .MinimumLength(9)
                .WithMessage("{PropertyName} must be at least 9 characters")
                .MaximumLength(15)
                .WithMessage("{PropertyName} must not exceed 15 characters");

            RuleFor(x => x.StorageId)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} must belong to some Storage");
        }
    }
}
