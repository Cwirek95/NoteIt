using FluentValidation;

namespace NoteIt.Application.Functions.Notes.Commands.CreateNote
{
    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        public CreateNoteCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(256)
                .WithMessage("{PropertyName} must not exceed 256 characters");

            RuleFor(x => x.Content)
                .MaximumLength(8000)
                .WithMessage("{PropertyName} must not exceed 8000 characters");

            RuleFor(x => x.IsImportant)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(x => x.StorageId)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} must belong to some Storage");

        }
    }
}
