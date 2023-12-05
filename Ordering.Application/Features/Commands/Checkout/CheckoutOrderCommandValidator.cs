using FluentValidation;

namespace Ordering.Application.Features.Commands.Checkout
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(x => x.Address)
                .EmailAddress()
                .NotEmpty().WithMessage("El correo no puede estar vacio, no sea malo!");

            RuleFor(x => x.UserName).MinimumLength(4).MaximumLength(20);

        }
    }
}
