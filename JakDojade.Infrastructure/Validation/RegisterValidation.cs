using System;
using FluentValidation;
using JakDojade.Core.Domain;
using JakDojade.Infrastructure.Commands;

namespace JakDojade.Core.Validation
{
    public class RegisterValidation : AbstractValidator<RegisterCommand>
    {
        public RegisterValidation()
        {
            RuleFor(user => user.Email)
                .EmailAddress();
            RuleFor(user => user.Password)
                .NotEmpty()
                .Length(8, 100);
            RuleFor(user => user.Username)
                .NotEmpty();
                
        }
    }
}
