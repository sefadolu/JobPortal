using JobPortal.Entities.DbContexts;
using JobPortal.Entities.Models.Concrete;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.DAL.Repository.Concrete
{
    public class EmployerRepository : Repository<Employer>
    {
        public EmployerRepository(JobDbContext context) : base(context)
        {
        }

        public async Task DeleteEmployerAsync(int employerId)
        {
            await DeleteAsync(employerId);
        }

        public async Task UpdateEmployerAsync(Employer employer)
        {
            await UpdateAsync(employer);
        }

        public async Task<IEnumerable<Job>> GetJobsByEmployerIdAsync(int employerId)
        {
            return await _context.Jobs
                .Where(job => job.EmployerId == employerId)
                .ToListAsync();
        }

        public async Task<CompanyProfile> GetCompanyProfileAsync(int employerId)
        {
            return await _context.CompanyProfiles.FirstOrDefaultAsync(cp => cp.Employer.Id == employerId);
        }

    }
}
