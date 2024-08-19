using JobPortal.BL.Abstract;
using JobPortal.DAL.Repository.Concrete;
using JobPortal.Entities.Models.Concrete;

namespace JobPortal.BL.Concrete
{
    public class JobSeekerManager : Manager<JobSeeker>, IJobSeekerManager
    {
        private readonly JobSeekerRepository _jobSeekerRepository;

        public JobSeekerManager(JobSeekerRepository jobSeekerRepository) : base(jobSeekerRepository)
        {
            _jobSeekerRepository = jobSeekerRepository;
        }

        public async Task<IEnumerable<Job>> GetAppliedJobsAsync(int jobSeekerId)
        {
            return await _jobSeekerRepository.GetAppliedJobsAsync(jobSeekerId);
        }

        public async Task<IEnumerable<Education>> GetEducationsAsync(int jobSeekerId)
        {
            return await _jobSeekerRepository.GetEducationsAsync(jobSeekerId);
        }
        public async Task<IEnumerable<Certification>> GetCertificationsAsync(int jobSeekerId)
        {
            return await _jobSeekerRepository.GetCertificationsAsync(jobSeekerId);
        }

    }
}
