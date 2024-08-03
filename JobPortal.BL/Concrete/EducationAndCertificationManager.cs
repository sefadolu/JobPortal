using JobPortal.BL.Abstract;
using JobPortal.DAL.Repository.Concrete;
using JobPortal.Entities.Models.Concrete;

namespace JobPortal.BL.Concrete
{
    public class EducationAndCertificationManager : Manager<EducationAndCertification>, IEducationAndCertificationManager
    {
        private readonly EducationAndCertificationRepository _educationAndCertificationRepository;

        public EducationAndCertificationManager(EducationAndCertificationRepository educationAndCertificationRepository) : base(educationAndCertificationRepository)
        {
            _educationAndCertificationRepository = educationAndCertificationRepository;
        }

        public async Task<IEnumerable<EducationAndCertification>> GetEducationAndCertificationsBySeekerIdAsync(int seekerId)
        {
            return await _educationAndCertificationRepository.GetEducationAndCertificationsBySeekerIdAsync(seekerId);
        }
    }
}
