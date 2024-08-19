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
    public class CertificationRepository : Repository<Certification>
    {
        public CertificationRepository(JobDbContext context) : base(context)
        {
        }

        // İş arayanın sertifika bilgilerini getirme
        public async Task<IEnumerable<Certification>> GetCertificationsBySeekerIdAsync(int seekerId)
        {
            return await _dbSet.Where(certification => certification.JobSeekerId == seekerId).ToListAsync();
        }
    }
}
