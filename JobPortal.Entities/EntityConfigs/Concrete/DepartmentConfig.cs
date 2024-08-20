using JobPortal.Entities.EntityConfigs.Abstract;
using JobPortal.Entities.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Entities.EntityConfigs.Concrete
{
    public class DepartmentConfig : BaseConfig<Department>
    {
        public override void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(d => d.Jobs)
                .WithOne(j => j.Department)
                .HasForeignKey(j => j.DepartmentId);

            builder.HasData(
                new Department { Id = 1, Name = "Akademik" },
                new Department { Id = 2, Name = "AR-GE" },
                new Department { Id = 3, Name = " Arşiv / Dokümantasyon" },
                new Department { Id = 4, Name = "Bakım / Onarım" },
                new Department { Id = 5, Name = "Bilgi İşlem" },
                new Department { Id = 6, Name = "Depo / Antrepo" },
                new Department { Id = 7, Name = "Eğitim" },
                new Department { Id = 8, Name = "Genel Başvuru" },
                new Department { Id = 9, Name = "Güvenlik" },
                new Department { Id = 10, Name = "Haberleşme" },
                new Department { Id = 11, Name = "Halkla İlişkiler" },
                new Department { Id = 12, Name = "Hizmet" },
                new Department { Id = 13, Name = "Hukuk" },
                new Department { Id = 14, Name = "İdari İşler" },
                new Department { Id = 15, Name = "İnsan Kaynakları" },
                new Department { Id = 16, Name = "İş Geliştirme" },
                new Department { Id = 17, Name = "İthalat / İhracat" },
                new Department { Id = 18, Name = "Kalite" },
                new Department { Id = 19, Name = "Lojistik" },
                new Department { Id = 20, Name = "Mimarlık" },
                new Department { Id = 21, Name = "Muhasebe" },
                new Department { Id = 22, Name = "Mühendislik" },
                new Department { Id = 23, Name = "Müşteri Hizmetleri / Çağrı Merkezi" },
                new Department { Id = 24, Name = "Müşteri İlişkileri" },
                new Department { Id = 25, Name = "Mütercim Tercümanlık" },
                new Department { Id = 26, Name = "Nakliye" },
                new Department { Id = 27, Name = "Operasyon" },
                new Department { Id = 28, Name = "Organizasyon" },
                new Department { Id = 29, Name = "Pazar Araştırma" },
                new Department { Id = 30, Name = "Pazarlama" },
                new Department { Id = 31, Name = "Personel" },
                new Department { Id = 32, Name = "Planlama" },
                new Department { Id = 33, Name = "Reklam" },
                new Department { Id = 34, Name = "Sağlık" },
                new Department { Id = 35, Name = "Satınalma" },
                new Department { Id = 36, Name = "Satış" },
                new Department { Id = 37, Name = "Sekreterya" },
                new Department { Id = 38, Name = "Spor" },
                new Department { Id = 39, Name = "Tasarım / Grafik" },
                new Department { Id = 40, Name = "Taşıma" },
                new Department { Id = 41, Name = "Teknikerlik" },
                new Department { Id = 42, Name = "Teknisyenlik" },
                new Department { Id = 43, Name = "Turizm" },
                new Department { Id = 44, Name = "Ulaştırma" },
                new Department { Id = 45, Name = "Üretim / İmalat" },
                new Department { Id = 46, Name = "Yönetim" },
                new Department { Id = 47, Name = "Finans" },
                new Department { Id = 48, Name = "Teknik" },
                new Department { Id = 49, Name = "Denetim / Teftiş" },
                new Department { Id = 50, Name = "Yiyecek ve İçecek" },
                new Department { Id = 51, Name = "Kredi" },
                new Department { Id = 52, Name = "Sigorta" },
                new Department { Id = 53, Name = "Ruhsatlandırma" },
                new Department { Id = 54, Name = "Program" },
                new Department { Id = 55, Name = "Teknoloji" },
                new Department { Id = 56, Name = "Dış İlişkiler" },
                new Department { Id = 57, Name = "Tedarik Yönetimi" },
                new Department { Id = 58, Name = "Sistem" },
                new Department { Id = 59, Name = "Risk Yönetimi" },
                new Department { Id = 60, Name = "Analiz / Araştırma" },
                new Department { Id = 61, Name = "Bireysel Portföy Yönetimi" },
                new Department { Id = 62, Name = "Borsa" },
                new Department { Id = 63, Name = "Borsa Finans" },
                new Department { Id = 64, Name = "Dış Denetim" },
                new Department { Id = 65, Name = "Hazine ve Sabit Getirili Menkul Değerler" },
                new Department { Id = 66, Name = "İç Denetim" },
                new Department { Id = 67, Name = "Kurumsal Finansman" },
                new Department { Id = 68, Name = "Uluslararası Sermaye Piyasası" },
                new Department { Id = 69, Name = "Varlık Yönetimi" },
                new Department { Id = 70, Name = "Yurtiçi Sermaye Piyasaları" },
                new Department { Id = 71, Name = "Gayrimenkul Değerleme" },
                new Department { Id = 72, Name = "Ön Büro" },
                new Department { Id = 73, Name = "Satış ve Pazarlama" },
                new Department { Id = 74, Name = "Bilgi Teknolojileri / IT" },
                new Department { Id = 75, Name = "Teknik Servis" },
                new Department { Id = 76, Name = "Pazarlama Teknolojileri" },
                new Department { Id = 77, Name = "Dijital Pazarlama" },
                new Department { Id = 78, Name = "İş Sağlığı ve Güvenliği" },
                new Department { Id = 79, Name = "Parça" },
                new Department { Id = 80, Name = "Müşteri Hizmetleri" },
                new Department { Id = 81, Name = "Satış Geliştirme" },
                new Department { Id = 82, Name = "E-Ticaret" },
                new Department { Id = 83, Name = "Yönetim, Risk ve Uyumluluk" },
                new Department { Id = 84, Name = "Laboratuvar" },
                new Department { Id = 85, Name = "Mali İşler" },
                new Department { Id = 86, Name = "Vize İşlemleri" },
                new Department { Id = 87, Name = "Diğer" }






            );
        }
    }
}
