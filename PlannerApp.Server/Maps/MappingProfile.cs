using AutoMapper;
using PlannerApp.Shared.Models;
using PlannerApp.Server.Models;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using PlannerApp.Server.Models.Identity;

namespace PlannerApp.Server.Maps
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PlannerItemDTO, PlannerItem>()
                .ForMember(d => d.tenantID, o => o.Ignore())
                .ForMember(d => d.Category, o => o.Ignore())
                .ReverseMap();
            CreateMap<CategoryDTO, Category>()
                .ForMember(d => d.PlannerItems, o => o.Ignore())
                .ForMember(d => d.tenantID, o => o.Ignore())
                .ReverseMap();
        }
    }
}
