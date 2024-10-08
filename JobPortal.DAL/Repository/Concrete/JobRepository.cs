﻿using JobPortal.Entities.DbContexts;
using JobPortal.Entities.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.DAL.Repository.Concrete
{
    public class JobRepository : Repository<Job>
    {
        public JobRepository(JobDbContext context) : base(context)
        {
        }

        public async Task DeleteJobAsync(int jobId)
        {
            await DeleteAsync(jobId);
        }

        public async Task<IEnumerable<Job>> GetJobsBySectorAsync(int sectorId)
        {
            return await _dbSet
                .Where(job => job.SectorId == sectorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsByDepartmentAsync(int departmentId)
        {
            return await _dbSet
                .Where(job => job.DepartmentId == departmentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsByJobTypeAsync(string jobType)
        {
            return await _dbSet
                .Where(job => job.JobType == jobType)
                .ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsByWorkTypeAsync(string workType)
        {
            return await _dbSet
                .Where(job => job.WorkType == workType)
                .ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsBySeekerIdAsync(int seekerId)
        {
            return await _context.Applications
                .Where(app => app.JobSeekerId == seekerId)
                .Select(app => app.Job)
                .ToListAsync();
        }

        public async Task<IEnumerable<JobSeeker>> GetJobSeekersByJobIdAsync(int jobId)
        {
            return await _context.Applications
                .Where(app => app.JobId == jobId)
                .Select(app => app.JobSeeker)
                .ToListAsync();
        }
    }
}
