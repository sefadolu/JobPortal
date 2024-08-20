using JobPortal.Entities.Models.Concrete;

namespace JobPortal.BL.Abstract
{
    public interface IJobManager : IManager<Job>
    {
        Task<IEnumerable<Job>> GetJobsBySectorAsync(int sectorId);

        Task<IEnumerable<Job>> GetJobsByDepartmentAsync(int departmentId);

        Task<IEnumerable<Job>> GetJobsByJobTypeAsync(string jobType);

        Task<IEnumerable<Job>> GetJobsByWorkTypeAsync(string workType);

        Task<IEnumerable<Job>> GetJobsBySeekerIdAsync(int seekerId);

        Task<IEnumerable<JobSeeker>> GetApplicantsForJobAsync(int jobId);
    }
}
