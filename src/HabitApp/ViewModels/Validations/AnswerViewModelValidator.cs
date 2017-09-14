using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace HabitApp.ViewModels.Validations
{
    public class AnswerViewModelValidator:AbstractValidator<AnswerViewModel>
    {
        public AnswerViewModelValidator()
        {
            RuleFor(a => a.AnswerDescription).NotEmpty().WithMessage("Description cannot be empty");
        }
    }
}
