using Data.Entities;
using FluentValidation;

namespace Data.Validators
{
    public class ServiceValidator : AbstractValidator<Service>
    {
        public ServiceValidator(DataContext dataContext)
        {
            RuleFor(x => x.ServiceTime)
                .NotEmpty();

            RuleFor(x => x.Name).MinimumLength(5).NotEmpty();

            RuleFor(x => x.Description).MinimumLength(10).NotEmpty();
        }
    }
}
