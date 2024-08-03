using JobPortal.Entities.DbContexts;
using JobPortal.Entities.Models.Concrete;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.DAL.Repository.Concrete
{
    public class CompanyProfileRepository : Repository<CompanyProfile>
    {
        public CompanyProfileRepository(JobDbContext context) : base(context)
        {
        }

        // Şirket profilini getirme
        public async Task<CompanyProfile> GetCompanyProfileByEmployerIdAsync(int employerId)
        {
            return await _dbSet.FirstOrDefaultAsync(profile => profile.Employer.Id == employerId);

        }
    }
}
