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
    public class CategoryRepository : Repository<Category>
    {
        public CategoryRepository(JobDbContext context) : base(context)
        {
        }

        // Tüm kategorileri getiren metod
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
