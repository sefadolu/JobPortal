using JobPortal.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Entities.Models.Concrete
{
    public class Education : BaseEntity
    {
        public string SchoolName { get; set; } 
        public string Department { get; set; } 
        public string GraduationDegree { get; set; } 
        public string Status { get; set; } 
        public int GraduationYear { get; set; } 
        public int JobSeekerId { get; set; } 
        public JobSeeker JobSeeker { get; set; } 
    }
}
