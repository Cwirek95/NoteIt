﻿using FluentValidation;

namespace NoteIt.Application.Functions.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(256)
                .WithMessage("{PropertyName} must not exceed 256 characters");

            RuleFor(x => x.Location)
                .MaximumLength(256)
                .WithMessage("{PropertyName} must not exceed 256 characters");

            RuleFor(x => x.Description)
                .MaximumLength(4000)
                .WithMessage("{PropertyName} must not exceed 4000 characters");

            RuleFor(x => x.StartDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .GreaterThanOrEqualTo(x => x.ReminderDate)
                .WithMessage("Start date must be later than reminder date");

            RuleFor(x => x.EndDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("End date must be later than start date");

            RuleFor(x => x.StorageAddress)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} must belong to some Storage");
        }
    }
}
