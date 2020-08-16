using Application.Categories.Queries.Common;
using AutoMapper;
using Domain.Categories;

namespace Application.Categories.Common
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CategoryModel, Category>()
                .ForMember(d => d.PlannerItems, o => o.Ignore())
                .ForMember(d => d.TenantID, o => o.Ignore())
                .ReverseMap();
        }
    }
}
