using AutoMapper;
using PlannerApp.Shared.Models;
using PlannerApp.Server.Models;

namespace PlannerApp.Server.Maps
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PlannerItemDTO, PlannerItem>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();
        }
    }
}
