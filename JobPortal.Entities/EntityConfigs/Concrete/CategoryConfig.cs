using JobPortal.Entities.EntityConfigs.Abstract;
using JobPortal.Entities.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPortal.Entities.EntityConfigs.Concrete
{
    public class CategoryConfig : BaseConfig<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .HasMaxLength(500);

            builder.HasMany(c => c.Jobs)
                .WithOne(j => j.Category)
                .HasForeignKey(j => j.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Kategoriler silindiğinde iş ilanlarını etkileme
        }
    }
}
