using FluentValidation;

namespace NoteIt.Application.Functions.Storages.Commands.CreateStorage
{
    public class CreateStorageCommandValidator : AbstractValidator<CreateStorageCommand>
    {
        public CreateStorageCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MinimumLength(3)
                .WithMessage("{PropertyName} must be at least 3 characters")
                .MaximumLength(64)
                .WithMessage("{PropertyName} must not exceed 64 characters")
                .Matches(@"^[a-zA-Z0-9\s]+$")
                .WithMessage("{PropertyName} must contain only letters and numbers");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MinimumLength(3)
                .WithMessage("{PropertyName} must be at least 3 characters")
                .MaximumLength(64)
                .WithMessage("{PropertyName} must not exceed 64 characters")
                .Must(x => !x.Any(y => Char.IsWhiteSpace(y)))
                .WithMessage("{PropertyName} cannot contain whitespace");
        }
    }
}
