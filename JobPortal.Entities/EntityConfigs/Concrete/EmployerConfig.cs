using JobPortal.Entities.EntityConfigs.Abstract;
using JobPortal.Entities.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPortal.Entities.EntityConfigs.Concrete
{
    public class EmployerConfig : BaseConfig<Employer>
    {
        public override void Configure(EntityTypeBuilder<Employer> builder)
        {
            builder.ToTable("Employers");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);

            

            builder.HasOne(e => e.CompanyProfile)
                .WithOne(cp => cp.Employer)
                .HasForeignKey<Employer>(e => e.CompanyProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Jobs)
                .WithOne(j => j.Employer)
                .HasForeignKey(j => j.EmployerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
