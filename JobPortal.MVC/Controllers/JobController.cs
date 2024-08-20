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
                }).ToListAsync(),

                JobTypes = new List<SelectListItem>
        {
            new SelectListItem { Value = "Tam zamanlı", Text = "Tam zamanlı" },
            new SelectListItem { Value = "Yarı zamanlı", Text = "Yarı zamanlı" },
            new SelectListItem { Value = "Stajyer", Text = "Stajyer" }
        },
                WorkTypes = new List<SelectListItem>
        {
            new SelectListItem { Value = "Remote", Text = "Remote" },
            new SelectListItem { Value = "Hibrit", Text = "Hibrit" },
            new SelectListItem { Value = "Yerinde", Text = "Yerinde" }
        }
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

                model.JobTypes = new List<SelectListItem>
        {
            new SelectListItem { Value = "Tam Zamanlı", Text = "Tam Zamanlı" },
            new SelectListItem { Value = "Yarı Zamanlı", Text = "Yarı Zamanlı" },
            new SelectListItem { Value = "Stajyer", Text = "Stajyer" }
        };
                model.WorkTypes = new List<SelectListItem>
        {
            new SelectListItem { Value = "Remote", Text = "Remote" },
            new SelectListItem { Value = "Hibrit", Text = "Hibrit" },
            new SelectListItem { Value = "Ofis", Text = "Ofis" }
        };

                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            var employer = await _context.Employers.FirstOrDefaultAsync(e => e.Email == user.Email);

            if (employer == null)
            {
                ModelState.AddModelError("", "İşveren bulunamadı.");
                return View(model);
            }

            model.Job.EmployerId = employer.Id;
            model.Job.PostedDate = DateTime.Now;
            model.Job.SectorId = model.SectorId;
            model.Job.DepartmentId = model.DepartmentId;
            model.Job.WorkType = model.WorkType; 
            model.Job.JobType = model.JobType;  

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
                .Include(j => j.Sector)
                .Include(j => j.Department)
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

        // İlan düzenleme formunu yükleme
        [HttpGet]
        public async Task<IActionResult> EditJob(int id)
        {
            var job = await _context.Jobs.FirstOrDefaultAsync(j => j.Id == id);

            if (job == null)
            {
                return NotFound("İlan bulunamadı.");
            }

            var jobViewModel = new JobViewModel
            {
                Job = job,
                SectorId = job.SectorId,
                DepartmentId = job.DepartmentId,
                Sectors = await _context.Sectors.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToListAsync(),
                Departments = await _context.Departments.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                }).ToListAsync(),

                JobTypes = new List<SelectListItem>
        {
            new SelectListItem { Value = "Tam Zamanlı", Text = "Tam Zamanlı", Selected = job.JobType == "Tam Zamanlı" },
            new SelectListItem { Value = "Yarı Zamanlı", Text = "Yarı Zamanlı", Selected = job.JobType == "Yarı Zamanlı" },
            new SelectListItem { Value = "Stajyer", Text = "Stajyer", Selected = job.JobType == "Stajyer" }
        },
                WorkTypes = new List<SelectListItem>
        {
            new SelectListItem { Value = "Remote", Text = "Remote", Selected = job.WorkType == "Remote" },
            new SelectListItem { Value = "Hibrit", Text = "Hibrit", Selected = job.WorkType == "Hibrit" },
            new SelectListItem { Value = "Ofis", Text = "Ofis", Selected = job.WorkType == "Ofis" }
        }
            };

            return View(jobViewModel);
        }

        // İlan düzenleme işlemi
        [HttpPost]
        public async Task<IActionResult> EditJob(JobViewModel model)
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

                model.JobTypes = new List<SelectListItem>
        {
            new SelectListItem { Value = "Tam Zamanlı", Text = "Tam Zamanlı" },
            new SelectListItem { Value = "Yarı Zamanlı", Text = "Yarı Zamanlı" },
            new SelectListItem { Value = "Stajyer", Text = "Stajyer" }
        };
                model.WorkTypes = new List<SelectListItem>
        {
            new SelectListItem { Value = "Remote", Text = "Remote" },
            new SelectListItem { Value = "Hibrit", Text = "Hibrit" },
            new SelectListItem { Value = "Ofis", Text = "Ofis" }
        };

                return View(model);
            }

            var job = await _context.Jobs.FirstOrDefaultAsync(j => j.Id == model.Job.Id);
            if (job == null)
            {
                return NotFound("İlan bulunamadı.");
            }

            job.Title = model.Job.Title;
            job.Description = model.Job.Description;
            job.Location = model.Job.Location;
            job.Salary = model.Job.Salary;
            job.SectorId = model.SectorId;
            job.DepartmentId = model.DepartmentId;
            job.JobType = model.JobType; 
            job.WorkType = model.WorkType; 



            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();

            return RedirectToAction("JobDetails", new { id = job.Id });
        }

        [Authorize(Roles = "Employer")]
        [HttpGet]
        public async Task<IActionResult> Applicants(int jobId)
        {
            var user = await _userManager.GetUserAsync(User);
            var employer = await _context.Employers.FirstOrDefaultAsync(e => e.Email == user.Email);

            if (employer == null)
            {
                return NotFound("İşveren bulunamadı.");
            }

            var job = await _context.Jobs
                .Include(j => j.Applications)
                .ThenInclude(a => a.JobSeeker)
                .ThenInclude(js => js.Educations)
                .Include(j => j.Applications)
                .ThenInclude(a => a.JobSeeker)
                .ThenInclude(js => js.Certifications)
                .FirstOrDefaultAsync(j => j.Id == jobId && j.EmployerId == employer.Id);

            if (job == null)
            {
                return NotFound("İlan bulunamadı.");
            }

            var applicants = job.Applications.Select(a => a.JobSeeker).ToList();

            return View(applicants);
        }

        [Authorize(Roles = "Employer")]
        [HttpPost]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var employer = await _context.Employers.FirstOrDefaultAsync(e => e.Email == user.Email);

            if (employer == null)
            {
                return NotFound("İşveren bulunamadı.");
            }

            var job = await _context.Jobs
                .FirstOrDefaultAsync(j => j.Id == id && j.EmployerId == employer.Id);

            if (job == null)
            {
                return NotFound("İlan bulunamadı.");
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return RedirectToAction("JobList", new { Message = "İlan başarıyla kaldırıldı." });
        }
    }
}
