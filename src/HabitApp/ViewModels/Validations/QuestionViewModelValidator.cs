using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace HabitApp.ViewModels.Validations
{
    public class QuestionViewModelValidator:AbstractValidator<QuestionViewModel>
    {
        public QuestionViewModelValidator()
        {
            RuleFor(q => q.QuestionDescription).NotEmpty().WithMessage("Decription cannot be empty");            
        }
    }
}
