using DigitalStore.Schema;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Validation
{
    internal class OrderValidator : AbstractValidator<OrderRequest>
    {
        public OrderValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.Address).NotEmpty().MinimumLength(2).MaximumLength(200);
            RuleFor(x => x.City).NotEmpty().MinimumLength(2).MaximumLength(200);
            RuleFor(x => x.PhoneNumber).NotNull().MinimumLength(10).MaximumLength(11);
            RuleFor(x => x.Email).EmailAddress().NotEmpty().MinimumLength(5).MaximumLength(100);
            RuleFor(x => x.Note).NotEmpty().MinimumLength(2).MaximumLength(300);

        }

       
    }
}
