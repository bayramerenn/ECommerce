using FluentValidation;

namespace ECommerceBasket.Features.Command.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.BasketId)
              .NotNull().NotEmpty()
                  .WithMessage("ID alanı boş geçilemez");
        }
    }
}
