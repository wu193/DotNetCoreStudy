using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using ModelValidatorSamples.Validators;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace ModelValidatorSamples.Models
{
    public class Customer //: IValidatableObject
    {
        public int ID { get; set; }

        [MinLength(12, ErrorMessage = "UserNameMinLength"), Required(ErrorMessage = "UserNameRequired"), Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required,EmailAddress]
        [Remote(action:"VerifyEmail",controller:"Customer")]
        [Display(Name ="Email")]
        public string Email { get; set; }

        [Birthday]
        public string Birthday { get; set; } //09-28

        public string LastName { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (!Regex.IsMatch(Birthday, @"\d{2}-\d{2}"))
        //    {
        //        yield return new ValidationResult("生日格式必须是月月-日日格式", new[] { nameof(Birthday),nameof(Email) });
        //    }
        //}
    }
}
