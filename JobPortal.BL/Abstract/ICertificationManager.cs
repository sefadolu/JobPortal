using JobPortal.Entities.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.BL.Abstract
{
    public interface ICertificationManager : IManager<Certification>
    {
        Task<IEnumerable<Certification>> GetCertificationsBySeekerIdAsync(int seekerId);
    }
}
