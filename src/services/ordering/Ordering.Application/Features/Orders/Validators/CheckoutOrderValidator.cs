namespace Ordering.Application.Features.Orders.Validators;

public sealed class CheckoutOrderValidator : AbstractValidator<CheckoutOrderCommand>
{
    public CheckoutOrderValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("User name is required")
            .MinimumLength(3)
            .WithMessage("User name must be at least 3 characters");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email format");

        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("Phone number is required")
            .Matches(@"^\+?[0-9]{10,15}$")
            .WithMessage("Invalid phone number");

        RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");

        RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required");

        RuleFor(x => x.State).NotEmpty().WithMessage("State is required");

        RuleFor(x => x.ZipCode)
            .NotEmpty()
            .WithMessage("Zip code is required")
            .Length(4, 10)
            .WithMessage("Zip code length is invalid");

        RuleFor(x => x.TotalPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Total price must be greater than 0");

        When(
            x => x.PaymentMethod == 1,
            () =>
            {
                RuleFor(x => x.CardName).NotEmpty().WithMessage("Card name is required");

                RuleFor(x => x.CardNumber)
                    .NotEmpty()
                    .WithMessage("Card number is required")
                    .CreditCard()
                    .WithMessage("Invalid card number");

                RuleFor(x => x.Expiration)
                    .NotEmpty()
                    .WithMessage("Expiration date is required")
                    .Matches(@"^(0[1-9]|1[0-2])\/\d{2}$")
                    .WithMessage("Expiration format must be MM/YY");

                RuleFor(x => x.CVV)
                    .NotEmpty()
                    .WithMessage("CVV is required")
                    .Length(3, 4)
                    .WithMessage("CVV must be 3 or 4 digits");
            }
        );
    }
}
