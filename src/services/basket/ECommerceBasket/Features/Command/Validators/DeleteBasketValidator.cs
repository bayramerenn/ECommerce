using FluentValidation;

namespace ECommerceBasket.Features.Command.Validators
{
    public class DeleteBasketValidator : AbstractValidator<DeleteBasketRequest>
    {
        public DeleteBasketValidator()
        {
            RuleFor(x => x.BasketId)
              .NotNull().NotEmpty()
                  .WithMessage("ID alanı boş geçilemez");
        }
    }
}
