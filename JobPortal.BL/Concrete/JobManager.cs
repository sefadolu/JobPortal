using JobPortal.BL.Abstract;
using JobPortal.DAL.Repository.Concrete;
using JobPortal.Entities.Models.Concrete;

namespace JobPortal.BL.Concrete
{
    public class JobManager : Manager<Job>, IJobManager
    {
        private readonly JobRepository _jobRepository;

        public JobManager(JobRepository jobRepository) : base(jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<IEnumerable<Job>> GetJobsByCategoryAsync(int categoryId)
        {
            return await _jobRepository.GetJobsByCategoryAsync(categoryId);
        }

        public async Task<IEnumerable<Job>> GetJobsBySeekerIdAsync(int seekerId)
        {
            return await _jobRepository.GetJobsBySeekerIdAsync(seekerId);
        }

        public async Task<IEnumerable<JobSeeker>> GetApplicantsForJobAsync(int jobId)
        {
            return await _jobRepository.GetJobSeekersByJobIdAsync(jobId);
        }
    }
}
