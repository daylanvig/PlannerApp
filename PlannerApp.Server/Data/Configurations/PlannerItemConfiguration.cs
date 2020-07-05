using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlannerApp.Server.Models;

namespace PlannerApp.Server.Data.Configurations
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
            builder.Property<string>("tenantID").HasColumnName("TenantID");
            builder.HasQueryFilter(p => EF.Property<string>(p, "tenantID") == tenantID);
        }
    }
}
