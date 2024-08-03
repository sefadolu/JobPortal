using JobPortal.Entities.Models.Abstract;

namespace JobPortal.Entities.Models.Concrete
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Job> Jobs { get; set; }
    }
}
