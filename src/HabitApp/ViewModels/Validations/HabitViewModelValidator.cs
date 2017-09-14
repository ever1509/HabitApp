using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace HabitApp.ViewModels.Validations
{
    public class HabitViewModelValidator:AbstractValidator<HabitViewModel>
    {
        public HabitViewModelValidator()
        {
            RuleFor(h => h.HabitDescription).NotEmpty().WithMessage("Description habit cannot be empty");
        }
    }
}
