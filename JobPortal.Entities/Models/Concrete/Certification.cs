using JobPortal.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Entities.Models.Concrete
{
    public class Certification : BaseEntity
    {
        public string InstitutionName { get; set; } 
        public string CertificateName { get; set; } 
        public DateTime CertificationDate { get; set; } 
        public int JobSeekerId { get; set; } 
        public JobSeeker JobSeeker { get; set; } 
    }
}
