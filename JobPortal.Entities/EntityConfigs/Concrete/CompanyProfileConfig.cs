using JobPortal.Entities.EntityConfigs.Abstract;
using JobPortal.Entities.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPortal.Entities.EntityConfigs.Concrete
{
    public class CompanyProfileConfig : BaseConfig<CompanyProfile>
    {
        public override void Configure(EntityTypeBuilder<CompanyProfile> builder)
        {
            builder.ToTable("CompanyProfiles");

            builder.HasKey(cp => cp.Id);

            builder.Property(cp => cp.CompanyName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(cp => cp.Description)
                .HasMaxLength(500);

            builder.Property(cp => cp.Website)
                .HasMaxLength(200);

            builder.Property(cp => cp.Location)
                .HasMaxLength(100);

            builder.HasOne(cp => cp.Employer)
                .WithOne(e => e.CompanyProfile)
                .HasForeignKey<Employer>(e => e.CompanyProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
