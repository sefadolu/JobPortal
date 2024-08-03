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
    public class EducationAndCertificationRepository : Repository<EducationAndCertification>
    {
        public EducationAndCertificationRepository(JobDbContext context) : base(context)
        {
        }

        // Eğitim ve sertifikaları getirme
        public async Task<IEnumerable<EducationAndCertification>> GetEducationAndCertificationsBySeekerIdAsync(int seekerId)
        {
            return await _dbSet.Where(education => education.JobSeekerId == seekerId).ToListAsync();
        }
    }
}
