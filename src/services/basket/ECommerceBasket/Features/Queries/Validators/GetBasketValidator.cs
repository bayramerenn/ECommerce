using FluentValidation;

namespace ECommerceBasket.Features.Queries.Validators
{
    public class GetBasketValidator : AbstractValidator<GetByIdOrderRequest>
    {
        public GetBasketValidator()
        {
            RuleFor(x => x.Id)
              .NotNull().NotEmpty()
                  .WithMessage("ID alanı boş geçilemez");
        }
    }
}