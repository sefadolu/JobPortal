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
    public class SectorManager : Manager<Sector>, ISectorManager
    {
        private readonly SectorRepository _sectorRepository;

        public SectorManager(SectorRepository sectorRepository) : base(sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }

        public async Task<IEnumerable<Sector>> GetAllSectorsAsync()
        {
            return await _sectorRepository.GetAllSectorsAsync();
        }
    }
}
