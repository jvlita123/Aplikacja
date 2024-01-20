using Data.Dto_s;
using Data.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Data.Validators
{
    public class MyUserValidator : AbstractValidator<MyUserDto>
    {
        private readonly DataContext _dataContext;

        public MyUserValidator(DataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = _dataContext.User.Where(u => u.Email == value).ToList();
                    if (emailInUse.Where(x=>x.Email == value).Count() > 1  )
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                });

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Today);

            RuleFor(x => x.FirstName)
                .MaximumLength(25)
                .WithMessage("The First Name is too long");

            RuleFor(x => x.LastName)
                .MaximumLength(25)
                .WithMessage("The Last Name is too long");

            RuleFor(x => x.PhoneNumber)
                .MinimumLength(9).WithMessage("PhoneNumber must not be less than 9 characters.")
                .MaximumLength(9).WithMessage("PhoneNumber must not exceed 9 characters.");
        }
    }
}
