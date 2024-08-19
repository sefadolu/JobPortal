using JobPortal.Entities.EntityConfigs.Abstract;
using JobPortal.Entities.Models.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Entities.EntityConfigs.Concrete
{
    public class EducationConfig : BaseConfig<Education>
    {
        public override void Configure(EntityTypeBuilder<Education> builder)
        {
            builder.ToTable("Educations");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.SchoolName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Department)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.GraduationDegree)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.GraduationYear)
                .IsRequired();

            builder.HasOne(e => e.JobSeeker)
                .WithMany(js => js.Educations) // JobSeeker'ın birden fazla eğitimi olabilir
                .HasForeignKey(e => e.JobSeekerId)
                .OnDelete(DeleteBehavior.Cascade); // JobSeeker silindiğinde ilgili eğitimler de silinir
        }
    }
}
