using FluentValidation;

namespace NoteIt.Application.Functions.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
    {
        public UpdateNoteCommandValidator()
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
        }
    }
}
