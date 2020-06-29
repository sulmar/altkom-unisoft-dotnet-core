using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.UniSoft.Models.Validators
{
    // dotnet add package FluentValidation
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().MinimumLength(3).MaximumLength(50);
            RuleFor(p => p.LastName).NotEmpty();
            RuleFor(p => p.Email).EmailAddress();
        }
    }
}
