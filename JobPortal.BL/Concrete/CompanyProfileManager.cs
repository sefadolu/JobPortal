using JobPortal.BL.Abstract;
using JobPortal.DAL.Repository.Concrete;
using JobPortal.Entities.Models.Concrete;

namespace JobPortal.BL.Concrete
{
    public class CompanyProfileManager : Manager<CompanyProfile>, ICompanyProfileManager
    {
        private readonly CompanyProfileRepository _companyProfileRepository;

        public CompanyProfileManager(CompanyProfileRepository companyProfileRepository) : base(companyProfileRepository)
        {
            _companyProfileRepository = companyProfileRepository;
        }

        public async Task<CompanyProfile> GetCompanyProfileByEmployerIdAsync(int employerId)
        {
            return await _companyProfileRepository.GetCompanyProfileByEmployerIdAsync(employerId);
        }
    }
}
