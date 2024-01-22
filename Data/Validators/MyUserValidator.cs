using Data.Dto_s;
using FluentValidation;

namespace Data.Validators
{
    public class MyUserValidator : AbstractValidator<MyUserDto>
    {
        public MyUserValidator()
        {
            RuleFor(x => x.UserDateOfBirth)
                .LessThan(DateTime.Today)
                .WithMessage("Date of birth must be less than today");


            RuleFor(x => x.FirstName)
                .MaximumLength(25)
                .WithMessage("The First Name is too long");

            RuleFor(x => x.LastName)
                .MaximumLength(25)
                .WithMessage("The Last Name is too long");

            RuleFor(x => x.PhoneNumber)
                .MinimumLength(9).WithMessage("Please enter a valid phone number")
                .MaximumLength(9).WithMessage("Please enter a valid phone number");
        }
    }
}
