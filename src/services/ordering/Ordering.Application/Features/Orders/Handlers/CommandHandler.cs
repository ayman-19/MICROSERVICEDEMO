using Ordering.Application.Exceptions;

namespace Ordering.Application.Features.Orders.Handlers;

public sealed class OrderCommandHandler(
    IUnitOfWork unitOfWork,
    IOrderRepository orderRepository,
    IMapper mapper
)
    : IRequestHandler<CheckoutOrderCommand, ResponseOf<OrderDto>>,
        IRequestHandler<UpdateOrderCommand, ResponseOf<OrderDto>>,
        IRequestHandler<DeleteOrderByIdCommand, ResponseOf<OrderDto>>
{
    public async Task<ResponseOf<OrderDto>> Handle(
        CheckoutOrderCommand request,
        CancellationToken cancellationToken
    )
    {
        int modifiedRows = 0;
        var strategy = unitOfWork.CreateExecutionStrategy();
        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            var order = mapper.Map<Order>(request);
            order.CreatorId = 1;
            await orderRepository.CreateAsync(order, cancellationToken);
            modifiedRows++;
            var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);
            if (success)
            {
                await transaction.CommitAsync(cancellationToken);
                return new ResponseOf<OrderDto>() { Result = mapper.Map<OrderDto>(order) };
            }
            await transaction.RollbackAsync(cancellationToken);
            throw new ServiceException("Occured exceptions.");
        });
    }

    public async Task<ResponseOf<OrderDto>> Handle(
        UpdateOrderCommand request,
        CancellationToken cancellationToken
    )
    {
        int modifiedRows = 0;
        await using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        var order = await orderRepository.FindAsync(request.Id, cancellationToken);
        mapper.Map(order, request);
        order.UpdatorId = 2;
        orderRepository.Update(order);
        modifiedRows++;
        var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);
        if (success)
        {
            await transaction.CommitAsync(cancellationToken);
            return new() { Result = mapper.Map<OrderDto>(order) };
        }
        await transaction.RollbackAsync(cancellationToken);
        throw new ServiceException("Occured exceptions.");
    }

    public async Task<ResponseOf<OrderDto>> Handle(
        DeleteOrderByIdCommand request,
        CancellationToken cancellationToken
    )
    {
        int modifiedRows = 0;
        await using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        var order = await orderRepository.FindAsync(request.Id, cancellationToken);
        orderRepository.Delete(order);
        modifiedRows++;
        var success = await unitOfWork.SaveChangesAsync(modifiedRows, cancellationToken);
        if (success)
        {
            await transaction.CommitAsync(cancellationToken);
            return new() { Result = mapper.Map<OrderDto>(order) };
        }
        await transaction.RollbackAsync(cancellationToken);
        throw new ServiceException("Occured exceptions.");
    }
}
