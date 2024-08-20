using JobPortal.Entities.DbContexts;
using JobPortal.Entities.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.DAL.Repository.Concrete
{
    public class ApplicationRepository : Repository<Application>
    {
        public ApplicationRepository(JobDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Application>> GetApplicationsByJobIdAsync(int jobId)
        {
            return await _dbSet.Where(app => app.JobId == jobId).ToListAsync();
        }

        public async Task<IEnumerable<Application>> GetApplicationsBySeekerIdAsync(int seekerId)
        {
            return await _dbSet.Where(app => app.JobSeekerId == seekerId).ToListAsync();
        }
    }
}
