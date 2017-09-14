using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HabitApp.Models.EntityLayer;
using HabitApp.ViewModels.Validations;

namespace HabitApp.ViewModels
{
    public class QuestionViewModel:IValidatableObject
    {               
        public int QuestionId { get; set; }
        public string QuestionDescription { get; set; }
        public DateTime? QuestionDate { get; set; }
        public int? HabitId { get; set; }        

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new QuestionViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(e => new ValidationResult(e.ErrorMessage, new[] { e.PropertyName }));
        }
    }
}
