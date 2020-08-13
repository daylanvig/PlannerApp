using Application.PlannerItems.Commands.Shared;
using Application.PlannerItems.Queries.Common;
using AutoMapper;
using Domain.PlannerItems;

namespace Application.PlannerItems.Common
{
    public class PlannerItemsMappingProfile : Profile
    {
        public PlannerItemsMappingProfile()
        {
            CreateMap<PlannerItemModel, PlannerItemCreateEditModel>();
            CreateMap<PlannerItemCreateEditModel, PlannerItem>()
                .ForMember(d => d.TenantID, o => o.Ignore())
                .ForMember(d => d.Category, o => o.Ignore());
            CreateMap<PlannerItemModel, PlannerItem>()
                .ForMember(d => d.TenantID, o => o.Ignore())
                .ForMember(d => d.Category, o => o.Ignore())
                .ReverseMap();
        }

    }
}
