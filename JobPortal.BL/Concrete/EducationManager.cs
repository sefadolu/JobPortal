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
    public class EducationManager : Manager<Education>, IEducationManager
    {
        private readonly EducationRepository _educationRepository;

        public EducationManager(EducationRepository educationRepository) : base(educationRepository)
        {
            _educationRepository = educationRepository;
        }

        public async Task<IEnumerable<Education>> GetEducationsBySeekerIdAsync(int seekerId)
        {
            return await _educationRepository.GetEducationsBySeekerIdAsync(seekerId);
        }
    }
}
