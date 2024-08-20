using JobPortal.Entities.Models.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public string? JobType { get; set; }
        public string? WorkType { get; set; }

        public int SectorId { get; set; }     
        public int DepartmentId { get; set; } 
        public Employer Employer { get; set; }
        public Sector Sector { get; set; }    
        public Department Department { get; set; } 
        public ICollection<Application> Applications { get; set; }
    }

}
