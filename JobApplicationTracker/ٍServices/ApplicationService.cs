
using JobApplicationTracker.Data;
using JobApplicationTracker.Models;

namespace JobApplicationTracker.Services;

public class ApplicationService
{
    private readonly DataStore<Application> _applicationDataStore;
    private readonly DataStore<Job> _jobDataStore;
    private readonly DataStore<Candidate> _candidateDataStore;

    private readonly HashSet<(int JobId, int CandidateId)> _applications;

    public ApplicationService(
        DataStore<Application> applicationDataStore,
        DataStore<Job> jobDataStore,
        DataStore<Candidate> candidateDataStore)
    {
        _applicationDataStore = applicationDataStore;
        _jobDataStore = jobDataStore;
        _candidateDataStore = candidateDataStore;

        _applications = new HashSet<(int JobId, int CandidateId)>();
    }

    public bool AddApplication(Application application, out string message)
    {
        if (application == null)
        {
            message = "Application cannot be null.";
            return false;
        }

        var job = _jobDataStore.GetById(application.JobId);

        if (job == null)
        {
            message = "Job not found.";
            return false;
        }

        var candidate = _candidateDataStore.GetById(application.CandidateId);

        if (candidate == null)
        {
            message = "Candidate not found.";
            return false;
        }

        var applicationKey = (application.JobId, application.CandidateId);

        if (_applications.Contains(applicationKey))
        {
            message = "Candidate has already applied for this job.";
            return false;
        }

        _applicationDataStore.Add(application);
        _applications.Add(applicationKey);

        message = "Application submitted successfully.";
        return true;
    }


    public Application? GetApplicationById(int id, out string message)
    {
        var application = _applicationDataStore.GetById(id);

        if (application == null)
        {
            message = "Application not found.";
            return null;
        }

        message = "Application found successfully.";
        return application;
    }

    
    public IEnumerable<Application> GetAllApplications()
    {
        return _applicationDataStore.GetAll();
    }

    public IEnumerable<Application> GetApplicationsByCandidate(int candidateId)
    {
        var result = new List<Application>();

        foreach (var application in _applicationDataStore.GetAll())
        {
            if (application.CandidateId == candidateId)
            {
                result.Add(application);
            }
        }

        return result;
    }

    public IEnumerable<Application> GetApplicationsByJob(int jobId)
    {
        var result = new List<Application>();

        foreach (var application in _applicationDataStore.GetAll())
        {
            if (application.JobId == jobId)
            {
                result.Add(application);
            }
        }

        return result;
    }
}
