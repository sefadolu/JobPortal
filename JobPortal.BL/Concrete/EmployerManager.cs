using JobPortal.BL.Abstract;
using JobPortal.DAL.Repository.Concrete;
using JobPortal.Entities.Models.Concrete;

namespace JobPortal.BL.Concrete
{
    public class EmployerManager : Manager<Employer>, IEmployerManager
    {
        private readonly EmployerRepository _employerRepository;

        public EmployerManager(EmployerRepository employerRepository) : base(employerRepository)
        {
            _employerRepository = employerRepository;
        }

        public async Task<IEnumerable<Job>> GetJobsByEmployerIdAsync(int employerId)
        {
            return await _employerRepository.GetJobsByEmployerIdAsync(employerId);
        }

        public async Task<CompanyProfile> GetCompanyProfileAsync(int employerId)
        {
            return await _employerRepository.GetCompanyProfileAsync(employerId);
        }
    }
}
