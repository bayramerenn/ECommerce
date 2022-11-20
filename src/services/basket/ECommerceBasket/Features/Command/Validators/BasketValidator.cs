using ECommerceBasket.Dtos;
using FluentValidation;

namespace ECommerceBasket.Features.Command.Validators
{
    public class BasketValidator : AbstractValidator<BasketDto>
    {
        public BasketValidator()
        {
            RuleFor(x => x.ProductName)
               .NotNull().NotEmpty()
                   .WithMessage("Ürün adı alanı boş geçilemez");
            RuleFor(x => x.BasketId)
               .NotNull().NotEmpty()
                   .WithMessage("Basket ID alanı boş geçilemez");
            RuleFor(x => x.CustomerId)
               .GreaterThan(0)
                    .WithMessage("Müşteri ID 0'dan büyük olmalıdır.");
            RuleFor(x => x.CreatedAt)
               .NotNull().NotEmpty()
                   .WithMessage("Tarih boş geçilemez");
            RuleFor(x => x.Quantity)
               .GreaterThan(0)
                   .WithMessage("Miktar 0'dan büyük olmalıdır.");
            RuleFor(x => x.Price)
               .GreaterThan(0)
                   .WithMessage("Fiyat 0'dan büyük olmalıdır.");
        }
    }
}
