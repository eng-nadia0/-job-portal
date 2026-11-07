using System;
using JobApplicationTracker.Data;
using JobApplicationTracker.Models;

namespace JobApplicationTracker.Services;
/*

if re.id!= job.recruiterId-> false   
   com.id!=re.companyId-> false

*/
public class JobService
{
    private readonly DataStore<Job> _jobDataStore;
    private readonly RecruiterService recruiters;
    private readonly CompanyService companies;

    public JobService(DataStore<Job> jobDataStore, RecruiterService recruiters, CompanyService companies)
    {
        _jobDataStore = jobDataStore;
        this.recruiters = recruiters;
        this.companies = companies;
    }

    public bool AddJob(Job job, out string message)
    {
        if (job == null)
        {
            message = "Invalid job data.";
            return false;
        }
        string ReMessage;
        var recruiter = recruiters.GetRecruiterById(job.RecruiterId, out ReMessage);
        if (recruiter == null)
        {
            message = ReMessage;
            return false;
        }
        var company = companies.GetById(recruiter.CompanyId);
        if (company == null)
        {
            message = "Associated company not found .";
            return false;
        }

        if (company.Status != CompanyStatus.Approved)
        {
            message = "Cannot add job for unapproved company.";
            return false;
        }
        if (job.CompanyId != company.Id)
        {
            message = "Job's company ID does not match the recruiter's company ID.";
            return false;
        }
        _jobDataStore.Add(job);
        message = $"Job '{job.Title}' added successfully.";
        return true;
    }

    public bool UpdateJob(Job job, out string message)
    {

        if(job == null)
        {
            message = "Invalid job data.";
            return false;
        }
        var existingJob = _jobDataStore.GetById(job.Id);
        if (existingJob == null)
        {
            message = "Job not found.";
            return false;
        }
        string ReMessage;
        var recruiter = recruiters.GetRecruiterById(job.RecruiterId, out ReMessage);
        if (recruiter == null)
        {
            message = ReMessage;
            return false;
        }
      
        var company = companies.GetById(recruiter.CompanyId);
        if (company == null)
        {
            message = "Associated company not found .";
            return false;
        }
        if(existingJob.CompanyId!=job.CompanyId)
        {
            message = "Cannot change the company of the job.";
            return false;
        }
        if(company.Status != CompanyStatus.Approved)
        {
            message = "Cannot update job for unapproved company.";
            return false;
        }
        if (job.CompanyId != company.Id)
        {
            message = "Job's company ID does not match the recruiter's company ID.";
            return false;
        }
        if (job == null)
        {
            message = "Invalid job data.";
            return false;
        }
        if (_jobDataStore.Update(job))
        {
            message = $"Job '{job.Title}' updated successfully.";
            return true;
        }
        else
        {
            message = "Failed to update job.";
            return false;
        }
    }

    public bool RemoveJob(int recruiterId, int jobId, out string message)
    {
        var existingJob = _jobDataStore.GetById(jobId);
        if (existingJob == null)
        {
            message = "Job not found.";
            return false;
        }
        string ReMessage;
        var checkRecturiter=recruiters.GetRecruiterById(recruiterId,out ReMessage);
        if (checkRecturiter == null)
        {
         message=ReMessage;
         return false;   
        }
        if (checkRecturiter.CompanyId != existingJob.CompanyId)
        {
            message = "You are not authorized to remove this job.";
            return false;
        }
        _jobDataStore.Remove(jobId);
        message = $"Job '{existingJob.Title}' removed successfully.";
        return true;
    }




}
