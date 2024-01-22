using Data.Entities;
using FluentValidation;

namespace Data.Validators
{
    public class CourseValidator : AbstractValidator<Course>
    {
        public CourseValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .MinimumLength(25)
                .MaximumLength(500);

            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(150);
        }
    }
}
