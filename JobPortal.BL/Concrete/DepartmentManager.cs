using JobPortal.BL.Abstract;
using JobPortal.DAL.Repository.Concrete;
using JobPortal.Entities.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.BL.Concrete
{
    public class DepartmentManager : Manager<Department>, IDepartmentManager
    {
        private readonly DepartmentRepository _departmentRepository;

        public DepartmentManager(DepartmentRepository departmentRepository) : base(departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _departmentRepository.GetAllDepartmentsAsync();
        }
    }
}
