using FluentValidation;
using ModelValidatorSamples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelValidatorSamples.Validators
{
    public class CustomValidator : AbstractValidator<Customer>
    {
        public CustomValidator()
        {
            RuleFor(x => x.Email).Length(20,30);
        }
    }
}
