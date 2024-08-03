using JobPortal.Entities.Models.Concrete;

namespace JobPortal.BL.Abstract
{
    public interface IEmployerManager : IManager<Employer>
    {
        Task<IEnumerable<Job>> GetJobsByEmployerIdAsync(int employerId);
        Task<CompanyProfile> GetCompanyProfileAsync(int employerId);
    }
}
