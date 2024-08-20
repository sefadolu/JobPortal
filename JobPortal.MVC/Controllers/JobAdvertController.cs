using JobPortal.Entities.DbContexts;
using JobPortal.Entities.Models.Concrete;
using JobPortal.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.MVC.Controllers
{
    public class JobAdvertController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JobDbContext _context;

        public JobAdvertController(UserManager<IdentityUser> userManager, JobDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // İş ilanlarını görüntüleme
        [HttpGet]
        public async Task<IActionResult> JobList(string search, int? sectorId, int? departmentId)
        {
            var jobs = _context.Jobs.AsQueryable();

            // Filtreleme işlemleri
            if (!string.IsNullOrEmpty(search))
            {
                jobs = jobs.Where(j => j.Title.Contains(search) || j.Description.Contains(search));
            }

            if (sectorId.HasValue)
            {
                jobs = jobs.Where(j => j.SectorId == sectorId.Value);
            }

            if (departmentId.HasValue)
            {
                jobs = jobs.Where(j => j.DepartmentId == departmentId.Value);
            }

            var filteredJobs = await jobs
                .Include(j => j.Sector)
                .Include(j => j.Department)
                .Include(j => j.Employer)
                    .ThenInclude(e => e.CompanyProfile) 
                .ToListAsync();

            var jobViewModel = new JobViewModel
            {
                Jobs = filteredJobs,
                Sectors = await _context.Sectors.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToListAsync(),
                Departments = await _context.Departments.Select(d => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                }).ToListAsync(),
                AppliedJobIds = new List<int>()
            };

            if (User.Identity.IsAuthenticated && User.IsInRole("JobSeeker"))
            {
                var user = await _userManager.GetUserAsync(User);
                var jobSeeker = await _context.JobSeekers.FirstOrDefaultAsync(js => js.Email == user.Email);

                if (jobSeeker != null)
                {
                    jobViewModel.AppliedJobIds = await _context.Applications
                        .Where(a => a.JobSeekerId == jobSeeker.Id)
                        .Select(a => a.JobId)
                        .ToListAsync();
                }
            }

            return View(jobViewModel);
        }

        // İş ilanı detayları
        [HttpGet]
        public async Task<IActionResult> JobDetails(int id)
        {
            var job = await _context.Jobs
                .Include(j => j.Sector)
                .Include(j => j.Department)
                .Include(j => j.Employer)
                    .ThenInclude(e => e.CompanyProfile) 
                .FirstOrDefaultAsync(j => j.Id == id);

            if (job == null)
            {
                return NotFound("İlan bulunamadı.");
            }

            var jobViewModel = new JobViewModel
            {
                Job = job,
                AppliedJobIds = new List<int>()
            };

            if (User.Identity.IsAuthenticated && User.IsInRole("JobSeeker"))
            {
                var user = await _userManager.GetUserAsync(User);
                var jobSeeker = await _context.JobSeekers.FirstOrDefaultAsync(js => js.Email == user.Email);

                if (jobSeeker != null)
                {
                    jobViewModel.AppliedJobIds = await _context.Applications
                        .Where(a => a.JobSeekerId == jobSeeker.Id)
                        .Select(a => a.JobId)
                        .ToListAsync();
                }
            }

            return View(jobViewModel);
        }

        // Şirket profilini görüntüleme
        [HttpGet]
        public async Task<IActionResult> CompanyProfile(int employerId)
        {
            var employer = await _context.Employers
                .Include(e => e.CompanyProfile)
                .FirstOrDefaultAsync(e => e.Id == employerId);

            if (employer == null || employer.CompanyProfile == null)
            {
                return NotFound("Şirket profili bulunamadı.");
            }

            return View(employer.CompanyProfile); 
        }

        // Başvuru işlemi
        [HttpPost]
        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> Apply(int jobId)
        {
            var user = await _userManager.GetUserAsync(User);
            var jobSeeker = await _context.JobSeekers.FirstOrDefaultAsync(js => js.Email == user.Email);

            if (jobSeeker == null)
            {
                return RedirectToAction("Login", "JobSeekerLogin");
            }

            var existingApplication = await _context.Applications
                .FirstOrDefaultAsync(a => a.JobId == jobId && a.JobSeekerId == jobSeeker.Id);

            if (existingApplication != null)
            {
                ModelState.AddModelError("", "Bu ilana başvurdunuz.");
                return RedirectToAction("JobDetails", new { id = jobId });
            }

            var application = new Application
            {
                JobId = jobId,
                JobSeekerId = jobSeeker.Id,
                ApplicationDate = DateTime.Now,
                Status = "Pending"
            };

            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            return RedirectToAction("JobDetails", new { id = jobId });
        }
    }
}
