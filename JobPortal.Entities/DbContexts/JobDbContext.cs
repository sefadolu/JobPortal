using JobPortal.Entities.EntityConfigs.Concrete;
using JobPortal.Entities.Models.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace JobPortal.Entities.DbContexts
{
    public class JobDbContext : IdentityDbContext
    {


        public JobDbContext(DbContextOptions<JobDbContext> options) : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobSeeker> JobSeekers { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<EducationAndCertification> EducationAndCertifications { get; set; }
        public DbSet<CompanyProfile> CompanyProfiles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Sector> Sectors { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfigürasyon sınıflarını burada uygulayın
            modelBuilder.ApplyConfiguration(new JobConfig());
            modelBuilder.ApplyConfiguration(new JobSeekerConfig());
            modelBuilder.ApplyConfiguration(new EmployerConfig());
            modelBuilder.ApplyConfiguration(new ApplicationConfig());
            modelBuilder.ApplyConfiguration(new CompanyProfileConfig());
            modelBuilder.ApplyConfiguration(new EducationAndCertificationConfig());
            modelBuilder.ApplyConfiguration(new DepartmentConfig());
            modelBuilder.ApplyConfiguration(new SectorConfig());

            modelBuilder.Entity<JobSeeker>().HasData(
                new JobSeeker
                {
                    Id = 1,
                    FirstName = "Ali",
                    LastName = "Veli",
                    Email = "ali.veli@example.com",
                    PhoneNumber = "1234567890"
                    // Diğer gerekli alanlar...
                }
            );


        }

    }
}
