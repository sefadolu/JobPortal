using JobPortal.Entities.EntityConfigs.Abstract;
using JobPortal.Entities.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPortal.Entities.EntityConfigs.Concrete
{
    public class JobConfig : BaseConfig<Job>
    {
        public override void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable("Jobs");

            builder.HasKey(j => j.Id);

            builder.Property(j => j.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(j => j.Description)
                .IsRequired()
                .HasMaxLength(5000);

            builder.Property(j => j.Location)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(j => j.Salary)
                .HasColumnType("decimal(18,2)");

            builder.Property(j => j.PostedDate)
                .IsRequired();

            builder.Property(j => j.JobType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(j => j.WorkType)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(j => j.Employer)
                .WithMany(e => e.Jobs)
                .HasForeignKey(j => j.EmployerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(j => j.Sector)
                .WithMany(s => s.Jobs)
                .HasForeignKey(j => j.SectorId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(j => j.Department)
                .WithMany(d => d.Jobs)
                .HasForeignKey(j => j.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict); 


            builder.HasMany(j => j.Applications)
                .WithOne(a => a.Job)
                .HasForeignKey(a => a.JobId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
