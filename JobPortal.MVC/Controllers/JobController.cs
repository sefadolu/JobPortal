using JobPortal.Entities.DbContexts;
using JobPortal.Entities.Models.Concrete;
using JobPortal.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> AddJob()
        {
            var jobViewModel = new JobViewModel
            {
                Job = new Job(),
                Sectors = await _context.Sectors.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToListAsync(),
                Departments = await _context.Departments.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                }).ToListAsync()
            };

            return View(jobViewModel);
        }

        // İş ilanı verme işlemi
        [HttpPost]
        public async Task<IActionResult> AddJob(JobViewModel model)
        {
            if (model.SectorId == 0 || model.DepartmentId == 0)
            {
                ModelState.AddModelError("", "Geçerli bir sektör veya departman seçmediniz.");
                model.Sectors = await _context.Sectors.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToListAsync();
                model.Departments = await _context.Departments.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                }).ToListAsync();

                return View(model);
            }

            // Kullanıcı ve işveren bilgilerini al
            var user = await _userManager.GetUserAsync(User);
            var employer = await _context.Employers.FirstOrDefaultAsync(e => e.Email == user.Email);

            if (employer == null)
            {
                ModelState.AddModelError("", "İşveren bulunamadı.");
                return View(model);
            }

            model.Job.EmployerId = employer.Id;
            model.Job.PostedDate = DateTime.Now;
            // Seçilen SectorId ve DepartmentId'yi Job modeline atayın
            model.Job.SectorId = model.SectorId;
            model.Job.DepartmentId = model.DepartmentId;

            // Veritabanına ekle
            _context.Jobs.Add(model.Job);
            await _context.SaveChangesAsync();

            return RedirectToAction("JobList");
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
                .Include(j => j.Sector) // Sektörü de dahil edelim
                .Include(j => j.Department) // Departmanı da dahil edelim
                .ToListAsync();

            return View(jobs);
        }

        // İlan detaylarını görüntüleme
        [HttpGet]
        public async Task<IActionResult> JobDetails(int id)
        {
            var job = await _context.Jobs
                .Include(j => j.Sector)
                .Include(j => j.Department)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (job == null)
            {
                return NotFound("İlan bulunamadı.");
            }

            return View(job);
        }

        [Authorize(Roles = "Employer")]
        [HttpGet]
        public async Task<IActionResult> Applicants(int jobId)
        {
            // Giriş yapmış kullanıcıyı al
            var user = await _userManager.GetUserAsync(User);
            var employer = await _context.Employers.FirstOrDefaultAsync(e => e.Email == user.Email);

            if (employer == null)
            {
                return NotFound("İşveren bulunamadı.");
            }

            // İş ilanını ve başvuran adayları bul
            var job = await _context.Jobs
                .Include(j => j.Applications)
                .ThenInclude(a => a.JobSeeker)
                .FirstOrDefaultAsync(j => j.Id == jobId && j.EmployerId == employer.Id);

            if (job == null)
            {
                return NotFound("İlan bulunamadı."); // İlan bulunamazsa hata
            }

            var applicants = job.Applications.Select(a => a.JobSeeker).ToList();

            return View(applicants);
        }
    }
}
