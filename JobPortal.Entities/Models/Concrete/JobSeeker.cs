using JobPortal.Entities.Models.Abstract;

namespace JobPortal.Entities.Models.Concrete
{
    public class JobSeeker : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Resume { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Skills { get; set; }

        public ICollection<Application>? Applications { get; set; }
        public ICollection<EducationAndCertification>? EducationAndCertifications { get; set; } // Burada ilişki tanımlanıyor

    }
}
