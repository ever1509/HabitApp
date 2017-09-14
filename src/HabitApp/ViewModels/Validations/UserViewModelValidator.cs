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
}
