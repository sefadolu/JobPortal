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
    public class SectorConfig : BaseConfig<Sector>
    {
        public override void Configure(EntityTypeBuilder<Sector> builder)
        {
            builder.ToTable("Sectors");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(s => s.Jobs)
                .WithOne(j => j.Sector)
                .HasForeignKey(j => j.SectorId);

          
            builder.HasData(
                new Sector { Id = 1, Name = "Bilişim" },
                new Sector { Id = 2, Name = "Üretim / Endüstriyel Ürünler" },
                new Sector { Id = 3, Name = "Elektrik & Elektronik" },
                new Sector { Id = 4, Name = "Güvenlik" },
                new Sector { Id = 5, Name = "Enerji" },
                new Sector { Id = 6, Name = "Gıda" },
                new Sector { Id = 7, Name = "Kimya" },
                new Sector { Id = 8, Name = "Maden ve Metal Sanayi" },
                new Sector { Id = 9, Name = "Mobilya & Aksesuar" },
                new Sector { Id = 10, Name = "Ev Eşyaları" },
                new Sector { Id = 11, Name = "Orman Ürünleri" },
                new Sector { Id = 12, Name = "Ofis / Büro Malzemeleri" },
                new Sector { Id = 13, Name = "Otomotiv" },
                new Sector { Id = 14, Name = "Sağlık" },
                new Sector { Id = 15, Name = "Tarım / Ziraat" },
                new Sector { Id = 16, Name = "Taşımacılık" },
                new Sector { Id = 17, Name = "Tekstil" },
                new Sector { Id = 18, Name = "Telekomünikasyon" },
                new Sector { Id = 19, Name = "Turizm" },
                new Sector { Id = 20, Name = "Yapı" },
                new Sector { Id = 21, Name = "Topluluklar" },
                new Sector { Id = 22, Name = "Hizmet" },
                new Sector { Id = 23, Name = "Danışmanlık" },
                new Sector { Id = 24, Name = "Reklam ve Tanıtım" },
                new Sector { Id = 25, Name = "Eğitim" },
                new Sector { Id = 26, Name = "Finans - Ekonomi" },
                new Sector { Id = 27, Name = "Ticaret" },
                new Sector { Id = 28, Name = "Denizcilik" },
                new Sector { Id = 29, Name = "Eğlence - Kültür - Sanat" },
                new Sector { Id = 30, Name = "Basım - Yayın" },
                new Sector { Id = 31, Name = "Medya" },
                new Sector { Id = 32, Name = "Havacılık" },
                new Sector { Id = 33, Name = "Hızlı Tüketim Malları" },
                new Sector { Id = 34, Name = "Hayvancılık" },
                new Sector { Id = 35, Name = "Sigortacılık" },
                new Sector { Id = 36, Name = "Dayanıklı Tüketim Ürünleri" },
                new Sector { Id = 37, Name = "Atık Yönetimi ve Geri Dönüşüm" },
                new Sector { Id = 38, Name = "Arşiv Yönetimi ve Saklama" },
                new Sector { Id = 39, Name = "Perakende" },
                new Sector { Id = 40, Name = "Çevre" },
                new Sector { Id = 41, Name = "İletişim Danışmanlığı" },
                new Sector { Id = 42, Name = "Kaynak ve Kesme Ekipmanları" },
                new Sector { Id = 43, Name = "Gemi Yan Sanayi" },
                new Sector { Id = 44, Name = "Bina ve Site Yönetimi" },
                new Sector { Id = 45, Name = "Sondaj" },
                new Sector { Id = 46, Name = "Bilgi Teknolojileri" },
                new Sector { Id = 47, Name = "Dental" },
                new Sector { Id = 48, Name = "Organizasyon" },
                new Sector { Id = 49, Name = "Otoyol, Tünel ve Köprü İşletmeciliği" },
                new Sector { Id = 50, Name = "Diğer" }
            );
        }
    }
}
