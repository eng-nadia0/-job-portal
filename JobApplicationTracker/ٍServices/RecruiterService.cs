using System;
using JobApplicationTracker.Models;
using JobApplicationTracker.Data;
namespace JobApplicationTracker.Services;


public class RecruiterService
{
    private readonly DataStore<Recruiter> _recruiterDataStore;
    private readonly CompanyService companies;

    public RecruiterService(DataStore<Recruiter> recruiterDataStore, CompanyService companies)
    {
        _recruiterDataStore = recruiterDataStore;
        this.companies = companies;
    }

    public bool AddRecruiter(Recruiter recruiter, out string message)
    {
        if (recruiter == null)
        {
            message = "Invalid recruiter data.";
            return false;
        }
        var company = companies.GetById(recruiter.CompanyId);
        if (company == null)
        {
            message = $"Company with ID {recruiter.CompanyId} does not exist.";
            return false;
        }
        if (recruiter.Email == null || !recruiter.Email.Contains("@") || !recruiter.Email.Contains("."))
        {
            message = "Invalid email format or null value.";
            return false;
        }

        _recruiterDataStore.Add(recruiter);
        message = "Recruiter added successfully.";
        return true;
    }

    public bool updateRecruiter(int recruiterId, Recruiter updatedRecruiter, out string message)
    {
        var checkRecruiter = _recruiterDataStore.GetById(recruiterId);
        if (checkRecruiter == null)
        {
            message = $"Recruiter with ID {recruiterId} does not exist.";
            return false;
        }
        if (updatedRecruiter == null)
        {
            message = "Invalid recruiter data.";
            return false;
        }
        if (updatedRecruiter.Email == null || !updatedRecruiter.Email.Contains("@") || !updatedRecruiter.Email.Contains("."))
        {
            message = "Invalid email format or null value.";
            return false;
        }
        var company = companies.GetById(updatedRecruiter.CompanyId);
        if (company == null)
        {
            message = $"Company with ID {updatedRecruiter.CompanyId} does not exist.";
            return false;
        }
        updatedRecruiter.Id = recruiterId;
        _recruiterDataStore.Update(updatedRecruiter);
        message = "Recruiter updated successfully.";
        return true;
    }

    public bool RemoveRecruiter(int recruiterId, out string message)
    {
        var recruiter = _recruiterDataStore.GetById(recruiterId);
        if (recruiter == null)
        {
            message = $"Recruiter with ID {recruiterId} does not exist.";
            return false;
        }
        _recruiterDataStore.Remove(recruiterId);
        message = "Recruiter removed successfully.";
        return true;
    }

    public Recruiter? GetRecruiterById(int recruiterId, out string message)
    {
        var recruiter = _recruiterDataStore.GetById(recruiterId);
        if (recruiter == null)
        {
            message = $"Recruiter with ID {recruiterId} does not exist in System.";
            return null;
        }
        message = "Recruiter found.";
        return recruiter;
    }

    public IEnumerable<Recruiter> GetAllRecruiters()
    {
        return _recruiterDataStore.GetAll();
    }




}
