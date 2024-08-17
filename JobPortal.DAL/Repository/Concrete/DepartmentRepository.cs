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
    public class DepartmentRepository : Repository<Department>
    {
        public DepartmentRepository(JobDbContext context) : base(context)
        {
        }

        // Tüm departmanları getiren metod
        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
