using JobPortal.Entities.Models.Abstract;

namespace JobPortal.Entities.Models.Concrete
{
    public class Employer : BaseEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int? CompanyProfileId { get; set; }

        public CompanyProfile? CompanyProfile { get; set; }
        public ICollection<Job>? Jobs { get; set; }
    }
}
