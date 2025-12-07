namespace Catalog.Application.Mappers.Brands;

public sealed class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<Brand, BrandDto>().ReverseMap();
    }
}
