using JobPortal.Entities.Models.Concrete;

namespace JobPortal.BL.Abstract
{
    public interface IJobManager : IManager<Job>
    {
        Task<IEnumerable<Job>> GetJobsByCategoryAsync(int categoryId);
        Task<IEnumerable<Job>> GetJobsBySeekerIdAsync(int seekerId);
        Task<IEnumerable<JobSeeker>> GetApplicantsForJobAsync(int jobId);
    }
}
