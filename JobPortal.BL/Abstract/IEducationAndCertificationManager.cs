using JobPortal.Entities.Models.Concrete;

namespace JobPortal.BL.Abstract
{
    public interface IEducationAndCertificationManager : IManager<EducationAndCertification>
    {
        Task<IEnumerable<EducationAndCertification>> GetEducationAndCertificationsBySeekerIdAsync(int seekerId);
    }
}
