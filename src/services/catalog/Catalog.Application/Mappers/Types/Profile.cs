namespace Catalog.Application.Mappers.Types;

public sealed class TypeProfile : Profile
{
    public TypeProfile()
    {
        CreateMap<ProductType, TypeDto>().ReverseMap();
    }
}
