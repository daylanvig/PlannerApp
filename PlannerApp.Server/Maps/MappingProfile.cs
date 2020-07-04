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
            CreateMap<PlannerItemDTO, PlannerItem>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();
        }
    }
}
