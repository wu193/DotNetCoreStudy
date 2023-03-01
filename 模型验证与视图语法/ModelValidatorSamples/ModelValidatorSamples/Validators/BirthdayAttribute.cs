using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ModelValidatorSamples.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ModelValidatorSamples.Validators
{
    public class BirthdayAttribute: ValidationAttribute, IClientModelValidator
    {
        public override bool IsValid(object value)
        {
            return Regex.IsMatch(value.ToString(), @"^\d{2}-\d{2}$");
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context==null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-birthday", "不是有效的生日格式");
            context.Attributes.Add("data-val-birthday-regex", @"\d{2}-\d{2}");
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Customer customer = (Customer)validationContext.ObjectInstance;

            if (!Regex.IsMatch(customer.Birthday, @"^\d{2}-\d{2}$"))
            {
                return new ValidationResult("生日格式必须为月月-日日格式", new[] { nameof(customer.Birthday) });
            }

            return ValidationResult.Success;
        }
    }
}
