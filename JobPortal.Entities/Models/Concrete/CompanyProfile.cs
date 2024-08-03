using JobPortal.Entities.Models.Abstract;

namespace JobPortal.Entities.Models.Concrete
{
    public class CompanyProfile : BaseEntity
    {
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Location { get; set; }

        public Employer Employer { get; set; }
    }
}
