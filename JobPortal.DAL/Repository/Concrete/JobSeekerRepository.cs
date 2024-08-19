﻿using JobPortal.Entities.DbContexts;
using JobPortal.Entities.Models.Concrete;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.DAL.Repository.Concrete
{
    public class JobSeekerRepository : Repository<JobSeeker>
    {
        public JobSeekerRepository(JobDbContext context) : base(context)
        {
        }

        // İş Arayan hesabını silme
        public async Task DeleteJobSeekerAsync(int jobSeekerId)
        {
            await DeleteAsync(jobSeekerId);
        }

        // Profil bilgilerini güncelleme
        public async Task UpdateJobSeekerAsync(JobSeeker jobSeeker)
        {
            await UpdateAsync(jobSeeker);
        }

        // İş arayanın başvuruda bulunduğu iş ilanlarını getiren metod
        public async Task<IEnumerable<Job>> GetAppliedJobsAsync(int jobSeekerId)
        {
            return await _context.Applications
                .Where(app => app.JobSeekerId == jobSeekerId)
                .Select(app => app.Job)
                .ToListAsync();
        }

        public async Task<IEnumerable<Education>> GetEducationsAsync(int jobSeekerId)
        {
            return await _context.Educations
                .Where(e => e.JobSeekerId == jobSeekerId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Certification>> GetCertificationsAsync(int jobSeekerId)
        {
            return await _context.Certifications
                .Where(c => c.JobSeekerId == jobSeekerId)
                .ToListAsync();
        }
    }
}
