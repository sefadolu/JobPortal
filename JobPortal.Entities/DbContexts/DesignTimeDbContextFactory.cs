using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
namespace JobPortal.Entities.DbContexts
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<JobDbContext>
    {
        public JobDbContext CreateDbContext(string[] args)
        {
            // Proje dizinine göre uygun base path'i ayarlayın
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../JobPortal.MVC")) // Yolu ayarlayın
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<JobDbContext>();
            optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 23)));

            return new JobDbContext(optionsBuilder.Options);
        }
    }
}