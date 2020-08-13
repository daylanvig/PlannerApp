using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.PlannerItems;

namespace Persistence.PlannerItems
{
    public class PlannerItemConfiguration : IEntityTypeConfiguration<PlannerItem>
    {
        private readonly string tenantID;
        public PlannerItemConfiguration(string tenantID)
        {
            this.tenantID = tenantID;
        }
        public void Configure(EntityTypeBuilder<PlannerItem> builder)
        {
            builder.Property(b => b.Description)
                .IsRequired();
            builder.Property(b => b.PlannedActionDate)
                .IsRequired();
            builder.Property<string>("TenantID").HasColumnName("TenantID");
            builder.HasQueryFilter(p => EF.Property<string>(p, "TenantID") == tenantID);
        }
    }
}
