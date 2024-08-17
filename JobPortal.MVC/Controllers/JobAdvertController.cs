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
                }).ToListAsync()
            };

            return View(jobViewModel);
        }

        // İş ilanı detayları
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
