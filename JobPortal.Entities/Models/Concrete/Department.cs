using JobPortal.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Entities.Models.Concrete
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Job> Jobs { get; set; }  
    }
}
