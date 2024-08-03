using JobPortal.BL.Abstract;
using JobPortal.DAL.Repository.Concrete;
using JobPortal.Entities.Models.Concrete;

namespace JobPortal.BL.Concrete
{
    public class ApplicationManager : Manager<Application>, IApplicationManager
    {
        private readonly ApplicationRepository _applicationRepository;

        public ApplicationManager(ApplicationRepository applicationRepository) : base(applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<IEnumerable<Application>> GetApplicationsByJobIdAsync(int jobId)
        {
            return await _applicationRepository.GetApplicationsByJobIdAsync(jobId);
        }

        public async Task<IEnumerable<Application>> GetApplicationsBySeekerIdAsync(int seekerId)
        {
            return await _applicationRepository.GetApplicationsBySeekerIdAsync(seekerId);
        }
    }
}
