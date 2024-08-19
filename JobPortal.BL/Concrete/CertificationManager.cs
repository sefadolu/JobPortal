using JobPortal.BL.Abstract;
using JobPortal.DAL.Repository.Concrete;
using JobPortal.Entities.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.BL.Concrete
{
    public class CertificationManager : Manager<Certification>, ICertificationManager
    {
        private readonly CertificationRepository _certificationRepository;

        public CertificationManager(CertificationRepository certificationRepository) : base(certificationRepository)
        {
            _certificationRepository = certificationRepository;
        }

        public async Task<IEnumerable<Certification>> GetCertificationsBySeekerIdAsync(int seekerId)
        {
            return await _certificationRepository.GetCertificationsBySeekerIdAsync(seekerId);
        }
    }
}
