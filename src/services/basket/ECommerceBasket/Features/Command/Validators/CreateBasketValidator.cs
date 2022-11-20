using ECommerceBasket.Dtos;
using FluentValidation;

namespace ECommerceBasket.Features.Command.Validators
{
    public class CreateBasketValidator : AbstractValidator<CreateBasketRequest>
    {
        public CreateBasketValidator()
        {
            RuleForEach(x => x.Data).NotNull().WithMessage("Tüm alanları doldurunuz.");
            RuleForEach(x => x.Data).SetValidator(new BasketValidator());
        }
    }
}
