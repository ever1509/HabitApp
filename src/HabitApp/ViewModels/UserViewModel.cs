using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HabitApp.ViewModels.Validations;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HabitApp.ViewModels
{
    public class UserViewModel:IValidatableObject
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsLocked { get; set; }
        public string Password { get; set; }
        public DateTime? DateCreated { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator= new UserViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(e => new ValidationResult(e.ErrorMessage, new[] {e.PropertyName}));
        }
    }
}
