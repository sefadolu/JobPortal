using JobPortal.Entities.Models.Abstract;

namespace JobPortal.Entities.Models.Concrete
{
    public class EducationAndCertification : BaseEntity
    {
        public string InstitutionName { get; set; }
        public string Degree { get; set; }
        public DateTime GraduationDate { get; set; }
        public int JobSeekerId { get; set; }
        public JobSeeker JobSeeker { get; set; }
    }
}
