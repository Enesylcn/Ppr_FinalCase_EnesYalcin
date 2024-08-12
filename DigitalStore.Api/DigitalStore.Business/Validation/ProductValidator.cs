using DigitalStore.Schema;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Validation
{
    public class ProductValidator : AbstractValidator<ProductRequest>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(100);
            RuleFor(x => x.Features).NotEmpty().MinimumLength(2).MaximumLength(200);
            RuleFor(x => x.Description).NotEmpty().MinimumLength(2).MaximumLength(400);
            RuleFor(x => x.PointsEarningPercentage).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Stock).GreaterThanOrEqualTo(0);

        }
    }
}
