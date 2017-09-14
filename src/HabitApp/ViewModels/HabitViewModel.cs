using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HabitApp.Models.EntityLayer;
using HabitApp.ViewModels.Validations;

namespace HabitApp.ViewModels
{
    public class HabitViewModel:IValidatableObject
    {
        public int HabitId { get; set; }
        public string HabitDescription { get; set; }
        public DateTime? HabitDate { get; set; }      
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new HabitViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(e => new ValidationResult(e.ErrorMessage, new[] { e.PropertyName }));
        }
    }
}
