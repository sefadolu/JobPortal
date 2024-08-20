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
    public class CertificationConfig : BaseConfig<Certification>
    {
        public override void Configure(EntityTypeBuilder<Certification> builder)
        {
            builder.ToTable("Certifications");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.InstitutionName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.CertificateName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.CertificationDate)
                .IsRequired();

            builder.HasOne(c => c.JobSeeker)
                .WithMany(js => js.Certifications) 
                .HasForeignKey(c => c.JobSeekerId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
