using JobPortal.Entities.Models.Concrete;


namespace JobPortal.BL.Abstract
{
    public interface IApplicationManager : IManager<Application>
    {
        Task<IEnumerable<Application>> GetApplicationsByJobIdAsync(int jobId);
        Task<IEnumerable<Application>> GetApplicationsBySeekerIdAsync(int seekerId);
    }
}
