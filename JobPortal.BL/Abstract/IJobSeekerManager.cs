using JobPortal.Entities.Models.Concrete;

namespace JobPortal.BL.Abstract
{
    public interface IJobSeekerManager : IManager<JobSeeker>
    {
        Task<IEnumerable<Job>> GetAppliedJobsAsync(int jobSeekerId);
        Task<IEnumerable<EducationAndCertification>> GetEducationAndCertificationsAsync(int jobSeekerId);
    }
}
