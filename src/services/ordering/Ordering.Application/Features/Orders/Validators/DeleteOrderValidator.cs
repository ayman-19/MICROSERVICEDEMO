namespace Ordering.Application.Features.Orders.Validators;

public sealed class DeleteOrderValidator : AbstractValidator<DeleteOrderByIdCommand>
{
    public DeleteOrderValidator(IOrderRepository orderRepository)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Order Id must be greater than 0")
            .MustAsync(
                async (id, cancellationToken) =>
                    await orderRepository.AnyAsync(o => o.Id == id, cancellationToken)
            )
            .WithMessage("Order with the specified Id does not exist");
    }
}
