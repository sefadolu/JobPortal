using JobPortal.Entities.Models.Abstract;

namespace JobPortal.Entities.Models.Concrete
{
    public class Job : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public decimal Salary { get; set; }
        public DateTime PostedDate { get; set; }
        public int EmployerId { get; set; }

        public int SectorId { get; set; }     // Sektör ID
        public int DepartmentId { get; set; } // Departman ID
        public Employer Employer { get; set; }
        public Sector Sector { get; set; }    // Sektör ilişkisi
        public Department Department { get; set; } // Departman ilişkisi
        public ICollection<Application> Applications { get; set; }
    }

}
