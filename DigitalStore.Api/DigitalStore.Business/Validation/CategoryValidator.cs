using DigitalStore.Schema;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Business.Validation
{
    public class CategoryValidator : AbstractValidator<CategoryRequest>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.Url).NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.Tags).NotEmpty().MinimumLength(2).MaximumLength(150);
        }
    }
}
