using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Categories;

namespace Persistence.Categories
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        private readonly string tenantID;
        public CategoryConfiguration(string tenantID)
        {
            this.tenantID = tenantID;
        }
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(p => p.PlannerItems)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryID);

            builder.Property<string>("TenantID").HasColumnName("TenantID");
            builder.HasQueryFilter(p => EF.Property<string>(p, "TenantID") == tenantID);
        }
    }
}

