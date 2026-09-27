using AutoMapper;
using SubastaYa.Aplication.Dtos.Category;
using SubastaYa.Aplication.UseCases.Categories.Commands.CreateCommand;
using SubastaYa.Aplication.UseCases.Categories.Commands.UpdateCommand;
using SubastaYa.Domain.Entities;

namespace SubastaYa.Aplication.Mappings;

public class CategoryMapping : Profile
{
    public CategoryMapping()
    {
        CreateMap<Category, CategoryResponseDto>().ReverseMap();
        CreateMap<Category, CreateCategoryCommand>().ReverseMap();
        CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
    }
}
