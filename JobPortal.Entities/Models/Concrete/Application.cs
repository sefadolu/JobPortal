using JobPortal.Entities.Models.Abstract;

namespace JobPortal.Entities.Models.Concrete
{
    public class Application : BaseEntity
    {
        public int JobId { get; set; }
        public int JobSeekerId { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string Status { get; set; }

        public Job Job { get; set; }
        public JobSeeker JobSeeker { get; set; }
    }
}
