using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HabitApp.ViewModels.Validations;

namespace HabitApp.ViewModels
{
    public class LoginViewModel : IValidatableObject
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new LoginViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
