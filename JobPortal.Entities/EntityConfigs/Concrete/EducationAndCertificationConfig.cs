using JobPortal.Entities.EntityConfigs.Abstract;
using JobPortal.Entities.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPortal.Entities.EntityConfigs.Concrete
{
    public class EducationAndCertificationConfig : BaseConfig<EducationAndCertification>
    {
        public override void Configure(EntityTypeBuilder<EducationAndCertification> builder)
        {
            builder.ToTable("EducationAndCertifications");

            builder.HasKey(eac => eac.Id);

            builder.Property(eac => eac.InstitutionName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(eac => eac.Degree)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(eac => eac.GraduationDate)
                .IsRequired();

            builder.HasOne(eac => eac.JobSeeker)
                .WithMany(js => js.EducationAndCertifications)
                .HasForeignKey(eac => eac.JobSeekerId)
                .OnDelete(DeleteBehavior.Cascade); // JobSeeker silindiğinde ilgili eğitim ve sertifikaların da silinmesini sağlar
        }
    }
}
