using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HabitApp.ViewModels.Validations;

namespace HabitApp.ViewModels
{
    public class AnswerViewModel:IValidatableObject
    {
        public int AnswerId { get; set; }
        public int? QuestionId { get; set; }
        public string AnswerDescription { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new AnswerViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(e => new ValidationResult(e.ErrorMessage, new[] { e.PropertyName }));
        }
    }
}
