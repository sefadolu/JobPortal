using JobPortal.Entities.EntityConfigs.Abstract;
using JobPortal.Entities.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPortal.Entities.EntityConfigs.Concrete
{
    public class JobSeekerConfig : BaseConfig<JobSeeker>
    {
        public override void Configure(EntityTypeBuilder<JobSeeker> builder)
        {
            builder.ToTable("JobSeekers");

            builder.HasKey(js => js.Id);

            builder.Property(js => js.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(js => js.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(js => js.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(js => js.PhoneNumber)
                .HasMaxLength(15);

            builder.Property(js => js.Address)
                .HasMaxLength(500);

            

            builder.Property(js => js.Resume)
                .HasMaxLength(1000);

            builder.Property(js => js.ProfilePicture)
                .HasMaxLength(255); 

            builder.Property(js => js.Skills)
                .HasMaxLength(1000); 

            builder.HasMany(js => js.Applications)
                .WithOne(a => a.JobSeeker)
                .HasForeignKey(a => a.JobSeekerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(js => js.Educations)
                .WithOne(e => e.JobSeeker)
                .HasForeignKey(e => e.JobSeekerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(js => js.Certifications)
                .WithOne(c => c.JobSeeker)
                .HasForeignKey(c => c.JobSeekerId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
