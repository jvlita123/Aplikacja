using Data.Entities;
using FluentValidation;

namespace Data.Validators
{
    public class CycleValidator : AbstractValidator<Cycle>
    {
        public CycleValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .MinimumLength(25)
                .MaximumLength(500);

            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(150);

            RuleFor(x => x.StartDate)
                .Must((model, startDate) => startDate < model.EndDate)
                .WithMessage("Start date must be earlier than end date.");

            RuleFor(x => x.EndDate)
                .Must((model, endDate) => endDate > model.StartDate)
                .WithMessage("End date must be later than start date.");
        }
    }
}
