using Base.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application.Validation
{
    public class PersonValidator: AbstractValidator<Person>
    {
        public PersonValidator() { 
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("First Name is required.");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Last Name is required.");
            RuleFor(p => p.EmailID).NotEmpty().WithMessage("Email ID is required.")
                .EmailAddress().WithMessage("A valid email is required.");
            RuleFor(p => p.Birthdate).LessThan(DateTime.Now).WithMessage("Birthdate must be in the past.");

        }
    }
}
