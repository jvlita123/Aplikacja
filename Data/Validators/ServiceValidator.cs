using Data.Dto_s;
using Data.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
