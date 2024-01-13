using Data.Dto_s;
using Data.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Data.Validators
{
    public class LoginUserDtoValidator : AbstractValidator<LoginDto>
    {
        private readonly DataContext _dataContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public LoginUserDtoValidator(DataContext dataContext, IPasswordHasher<User> passwordHasher)
        {
            _dataContext = dataContext;
            _passwordHasher = passwordHasher;

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(x => x.Password)
                .Must((dto, password) => BeValidUser(dto.Email, password))
                .WithMessage("Invalid email or password.");
        }

        private bool BeValidUser(string email, string password)
        {
            var user = _dataContext.User.SingleOrDefault(u => u.Email == email);

            if (user != null)
            {
                var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
                return passwordVerificationResult == PasswordVerificationResult.Success;
            }

            return false;
        }
    }
}
