namespace Basket.Application.Mappers.CheckOuts;

public sealed class CheckOutProfile : Profile
{
    public CheckOutProfile()
    {
        CreateMap<CheckOutCommand, BasketCheckedOutEvent>();
    }
}
