using JobPortal.Entities.DbContexts;
using JobPortal.Entities.Models.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace JobPortal.MVC.Controllers
{
    [Authorize(Roles = "Employer")]
    public class JobController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JobDbContext _context;

        public JobController(UserManager<IdentityUser> userManager, JobDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // İş ilanı verme formu
        [HttpGet]
        public IActionResult AddJob()
        {
            // Sektör ve Departman verilerini getiriyoruz
            var sectors = _context.Sectors.AsNoTracking().ToList();
            var departments = _context.Departments.AsNoTracking().ToList();

            // View'de kullanmak için Sectors ve Departments verilerini ViewBag'e ekliyoruz
            ViewBag.Sectors = sectors;
            ViewBag.Departments = departments;

            var jobModel = new Job();
            return View(jobModel); // Boş bir Job modelini View'e gönderiyoruz
        }

        // İş ilanı verme işlemi
        [HttpPost]
        public async Task<IActionResult> AddJob(Job model, int SectorId, int DepartmentId)
        {
            // Sektör ve Departmanları tekrar ViewBag'e ekleyelim ki form hata durumunda kaybolmasın
            var sectors = _context.Sectors.AsNoTracking().ToList();
            var departments = _context.Departments.AsNoTracking().ToList();
            ViewBag.Sectors = sectors;
            ViewBag.Departments = departments;

            if (SectorId == 0 || DepartmentId == 0)
            {
                ModelState.AddModelError("", "Geçerli bir sektör veya departman seçmediniz.");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model); // Form hatalıysa aynı sayfayı tekrar göster
            }

            var user = await _userManager.GetUserAsync(User);
            var employer = await _context.Employers.FirstOrDefaultAsync(e => e.Email == user.Email);

            if (employer == null)
            {
                ModelState.AddModelError("", "İşveren bulunamadı."); // Hata mesajı ekleyin
                return View(model);
            }

            // Job modelini güncelleme
            model.EmployerId = employer.Id;  // İlanı işverene bağlama
            model.PostedDate = DateTime.Now; // İlanın verildiği tarih
            model.SectorId = SectorId;       // Seçilen sektör Id'sini atama
            model.DepartmentId = DepartmentId; // Seçilen departman Id'sini atama

            _context.Jobs.Add(model); // Yeni iş ilanı veritabanına ekleniyor
            await _context.SaveChangesAsync(); // Değişiklikler kaydediliyor

            return RedirectToAction("JobList"); // İlanlar listesine yönlendirme
        }

        // İşverenin ilanlarının listesi
        [HttpGet]
        public async Task<IActionResult> JobList()
        {
            var user = await _userManager.GetUserAsync(User);
            var employer = await _context.Employers.FirstOrDefaultAsync(e => e.Email == user.Email);

            if (employer == null)
            {
                return NotFound("İşveren bulunamadı.");
            }

            var jobs = await _context.Jobs
                .Where(j => j.EmployerId == employer.Id)
                .Include(j => j.Applications) // İlanlara gelen başvuruları da ekliyoruz
                .ToListAsync();

            return View(jobs); // İş ilanları listesi döndürülür
        }

        // İlan detaylarını görüntüleme
        [HttpGet]
        public async Task<IActionResult> JobDetails(int id)
        {
            var job = await _context.Jobs
                .Include(j => j.Applications)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (job == null)
            {
                return NotFound("İlan bulunamadı.");
            }

            return View(job); // İlan detayları döndürülür
        }
    }
}
