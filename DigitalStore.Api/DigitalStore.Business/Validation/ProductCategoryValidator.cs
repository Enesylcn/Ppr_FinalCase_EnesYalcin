using DigitalStore.Schema;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Validation
{
    public class ProductCategoryValidator : AbstractValidator<ProductCategoryRequest>
    {
        public ProductCategoryValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.CategoryId).NotEmpty().GreaterThan(0);
        }
    }
}
