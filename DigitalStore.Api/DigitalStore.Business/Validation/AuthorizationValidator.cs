using DigitalStore.Schema;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Validation
{
    public class AuthorizationValidator : AbstractValidator<AuthRequest>
    {
        public AuthorizationValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(3).MaximumLength(50).WithMessage("User name is required and must be min 2 character.");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(30).WithMessage("Password is required and must be min 6 character.");
        }
    }
}
