using JobPortal.Entities.Models.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace JobPortal.MVC.ViewModels
{
    public class JobViewModel
    {
        public Job Job { get; set; }

        [Required(ErrorMessage = "Sektör seçilmelidir.")]
        public int SectorId { get; set; }

        [Required(ErrorMessage = "Departman seçilmelidir.")]
        public int DepartmentId { get; set; }
        public List<SelectListItem> Sectors { get; set; }
        public List<SelectListItem> Departments { get; set; }

    }
}
