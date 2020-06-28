using AutoMapper;
using PlannerApp.Shared.Models;
using PlannerApp.Server.Models;

namespace PlannerApp.Server.Maps
{
    public class PlannerItemMappingProfile : Profile
    {
        public PlannerItemMappingProfile()
        {
            CreateMap<PlannerItemDTO, PlannerItem>().ReverseMap();
        }
    }
}
