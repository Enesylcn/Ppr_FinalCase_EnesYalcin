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
            // ProductIds liste içeriği doğrulama
            RuleFor(request => request.ProductIds)
                .NotNull().WithMessage("Product IDs list cannot be null.")
                .NotEmpty().WithMessage("Product IDs list cannot be empty.")
                .Must(productIds => productIds.All(id => id > 0))
                .WithMessage("All product IDs must be greater than zero.");

            // CouponCode doğrulama
            RuleFor(request => request.CouponCode)
                .Matches(@"^[A-Za-z0-9]{0,20}$").WithMessage("Coupon code must be alphanumeric and up to 20 characters long.")
                .When(request => !string.IsNullOrEmpty(request.CouponCode));
        }
    }
}
