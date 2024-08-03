using JobPortal.Entities.Models.Concrete;

namespace JobPortal.BL.Abstract
{
    public interface ICompanyProfileManager : IManager<CompanyProfile>
    {
        Task<CompanyProfile> GetCompanyProfileByEmployerIdAsync(int employerId);
    }
}
