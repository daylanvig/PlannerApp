using AutoMapper;
using Domain.Categories;
using PlannerApp.Shared.Models;

namespace PresentationServer.Categories
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CategoryDTO, Category>()
                .ForMember(d => d.PlannerItems, o => o.Ignore())
                .ForMember(d => d.TenantID, o => o.Ignore())
                .ReverseMap();
        }
    }
}
