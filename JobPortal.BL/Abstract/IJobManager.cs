using JobPortal.Entities.Models.Concrete;

namespace JobPortal.BL.Abstract
{
    public interface IJobManager : IManager<Job>
    {
        // Sektöre göre iş ilanlarını getiren metod
        Task<IEnumerable<Job>> GetJobsBySectorAsync(int sectorId);

        // Departmana göre iş ilanlarını getiren metod
        Task<IEnumerable<Job>> GetJobsByDepartmentAsync(int departmentId);

        // İş arayanın başvurduğu iş ilanlarını getiren metod
        Task<IEnumerable<Job>> GetJobsBySeekerIdAsync(int seekerId);

        // Belirli bir iş ilanına başvuran iş arayanları getiren metod
        Task<IEnumerable<JobSeeker>> GetApplicantsForJobAsync(int jobId);
    }
}
