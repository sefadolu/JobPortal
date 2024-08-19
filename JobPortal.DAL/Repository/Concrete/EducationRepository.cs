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
    public class EducationRepository : Repository<Education>
    {
        public EducationRepository(JobDbContext context) : base(context)
        {
        }

        // İş arayanın eğitim bilgilerini getirme
        public async Task<IEnumerable<Education>> GetEducationsBySeekerIdAsync(int seekerId)
        {
            return await _dbSet.Where(education => education.JobSeekerId == seekerId).ToListAsync();
        }
    }
}
