using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {
            RuleFor(user => user.SaleDate).NotEmpty();
            RuleFor(user => user.Customer).NotEmpty();
            RuleFor(user => user.Branch).NotEmpty();
            RuleFor(user => user.Items).NotEmpty();
            RuleForEach(sale => sale.Items).ChildRules(items =>
            {
                items.RuleFor(item => item.Product).NotEmpty();
                items.RuleFor(item => item.Quantity).GreaterThan(0);
                items.RuleFor(item => item.UnitPrice).GreaterThan(0);
            });
        }
    }
}
