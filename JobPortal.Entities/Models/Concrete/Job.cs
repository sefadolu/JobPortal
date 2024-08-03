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

        public int CategoryId { get; set; } // Bu özellik `Category` ile çakışabilir
        public Employer Employer { get; set; }
        public Category Category { get; set; } // İkinci kez `Category` tanımı
        public ICollection<Application> Applications { get; set; }
    }

}
