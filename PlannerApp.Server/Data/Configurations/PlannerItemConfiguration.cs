using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlannerApp.Server.Models;

namespace PlannerApp.Server.Data.Configurations
{
    public class PlannerItemConfiguration : IEntityTypeConfiguration<PlannerItem>
    {
        public void Configure(EntityTypeBuilder<PlannerItem> builder)
        {
            builder.Property(b => b.Description)
                .IsRequired();
            builder.Property(b => b.PlannedActionDate)
                .IsRequired();
        }
    }
}
