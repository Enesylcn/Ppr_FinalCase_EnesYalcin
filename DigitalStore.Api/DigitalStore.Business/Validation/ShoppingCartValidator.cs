using DigitalStore.Schema;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Validation
{
    public class ShoppingCartValidator : AbstractValidator<ShoppingCartRequest>
    {
        public ShoppingCartValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.CouponCode).NotEmpty().MinimumLength(2).MaximumLength(8);
        }
    }
}
