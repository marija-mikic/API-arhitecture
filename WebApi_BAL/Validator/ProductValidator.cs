using FluentValidation;
using WebApi_DAL.Models;

namespace WebApi_BAL.Validator
{
    public class ProductValidator: AbstractValidator<Product>
    {
        public ProductValidator() {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull().MaximumLength(30);
            RuleFor(x => x.Title).MaximumLength(100).NotEmpty().NotNull();
        
        
        }
    }
}
