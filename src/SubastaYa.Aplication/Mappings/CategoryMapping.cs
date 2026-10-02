using AutoMapper;
using SubastaYa.Aplication.Dtos.Category;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Mappings;

public class CategoryMapping : Profile
{
    public CategoryMapping()
    {
        CreateMap<Category, CategoryResponseDto>();

        CreateMap<CategoryCreateDto, Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Auctions, opt => opt.Ignore());
    }
}
