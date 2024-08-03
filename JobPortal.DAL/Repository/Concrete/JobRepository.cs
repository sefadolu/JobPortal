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
    public class JobRepository : Repository<Job>
    {
        public JobRepository(JobDbContext context) : base(context)
        {
        }

        // İş ilanını kaldırma
        public async Task DeleteJobAsync(int jobId)
        {
            await DeleteAsync(jobId);
        }

        // Kategoriye göre iş ilanlarını filtreleyen metod
        public async Task<IEnumerable<Job>> GetJobsByCategoryAsync(int categoryId)
        {
            return await _dbSet.Where(job => job.CategoryId == categoryId).ToListAsync();
        }

        // İş arayan tarafından başvurulmuş iş ilanlarını getiren metod
        public async Task<IEnumerable<Job>> GetJobsBySeekerIdAsync(int seekerId)
        {
            return await _context.Applications
                .Where(app => app.JobSeekerId == seekerId)
                .Select(app => app.Job)
                .ToListAsync();
        }
        // İş ilanına başvuran iş arayanların profillerini getiren metod
        public async Task<IEnumerable<JobSeeker>> GetJobSeekersByJobIdAsync(int jobId)
        {
            return await _context.Applications
                .Where(app => app.JobId == jobId)
                .Select(app => app.JobSeeker)
                .ToListAsync();
        }
    }
}
