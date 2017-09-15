using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace HabitApp.ViewModels.Validations
{
    public class UserViewModelValidator:AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            RuleFor(u => u.UserName).NotEmpty().WithMessage("Username cannot be empty");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Password cannot be empty");
        }
    }
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(l => l.Username).NotEmpty().WithMessage("Invalid username");
            RuleFor(l => l.Password).NotEmpty().WithMessage("Invalid password");
        }
    }
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(l => l.UserName).NotEmpty().WithMessage("Invalid username");
            RuleFor(l => l.Email).NotEmpty().WithMessage("Invalid email");
            RuleFor(l => l.Password).NotEmpty().WithMessage("Invalid password");
        }
    }
}
