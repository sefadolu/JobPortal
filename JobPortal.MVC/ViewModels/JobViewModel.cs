using JobPortal.Entities.Models.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace JobPortal.MVC.ViewModels
{
    public class JobViewModel
    {
        public Job Job { get; set; }
        public IEnumerable<Job> Jobs { get; set; }

        [Required(ErrorMessage = "Sektör seçilmelidir.")]
        public int SectorId { get; set; }
        public string CompanyName { get; set; }  // Şirket Adı

        [Required(ErrorMessage = "Departman seçilmelidir.")]
        public int DepartmentId { get; set; }
        public List<SelectListItem> Sectors { get; set; }
        public List<SelectListItem> Departments { get; set; }
        public List<int> AppliedJobIds { get; set; } // Başvurulan ilanların ID'lerini tutan liste



    }
}
