using AutoMapper;
using SubastaYa.Aplication.Dtos.Category;
using SubastaYa.Aplication.UseCases.Auctions.Commands.CreateCommand;
using SubastaYa.Aplication.UseCases.Categories.Commands.CreateCommand;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Mappings;

public class CategoryMapping : Profile
{
    public CategoryMapping()
    {
        CreateMap<Category, CategoryResponseDto>();

        CreateMap<CreateCategoryCommand, Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Auctions, opt => opt.Ignore())
            
            .ForMember(dest => dest.State, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());
    }
}
