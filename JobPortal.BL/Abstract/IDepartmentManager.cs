using JobPortal.Entities.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.BL.Abstract
{
    public interface IDepartmentManager : IManager<Department>
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
    }
}
