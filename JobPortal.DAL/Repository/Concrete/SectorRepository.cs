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
    public class SectorRepository : Repository<Sector>
    {
        public SectorRepository(JobDbContext context) : base(context)
        {
        }

        // Tüm sektörleri getiren metod
        public async Task<IEnumerable<Sector>> GetAllSectorsAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
