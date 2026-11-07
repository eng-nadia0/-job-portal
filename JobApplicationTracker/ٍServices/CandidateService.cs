using System;
using JobApplicationTracker.Models;
using JobApplicationTracker.Data;
namespace JobApplicationTracker.Services;

public class CandidateService
{
    private readonly DataStore<Candidate> _CandidateDataStore;
    private HashSet<String> _Emails;
    
    public CandidateService(DataStore<Candidate> candidates,HashSet<string> email)
    {
        _Emails=email;
        _CandidateDataStore=candidates;
    }

    public void AddCandidate(Candidate candidate)
    {
        if (candidate == null)
        {
            Console.WriteLine("Candidate canot be null");
            return;
        }
        if (candidate.Email == null || !candidate.Email.Contains('@') || !candidate.Email.Contains('.'))
        {
            Console.WriteLine("Wrong format or null value");
            return;
        }
      
        if (!_Emails.Contains(candidate.Email))
        {
          
           _CandidateDataStore.Add(candidate);
            _Emails.Add(candidate.Email);
        }
        else
        {
            Console.WriteLine("Email is already exit");
        }
    }
        public bool uodate(int id, Candidate candidate,out string message)
    {
        if (candidate == null)
        {
            message="candidate can't be null";
            return false;
        }
        var exitcandidate=_CandidateDataStore.GetById(id);
        if(exitcandidate==null){
            message="candidate coudn't be found";
            return false;
        }
        if (_CandidateDataStore.Update(candidate))
        {
            message="candidate has updated succesfuly.";
            return true;
        }
        else
        {
            message="candidate couldn't be updated:";
            return false;
        }
        
    }
    public bool RemoveCandidate(int id, out string message)
    {
        var candidate = _CandidateDataStore.GetById(id);

        if (candidate == null)
        {
            message = "Candidate not found.";
            return false;
        }

       _CandidateDataStore.Remove(id);
        _Emails.Remove(candidate.Email);
        message = "Candidate removed successfully.";
        return true;
    }

     public Candidate? GetCandidateById(int id, out string message)
    {
        var candidate = _CandidateDataStore.GetById(id);

        if (candidate == null)
        {
            message = "Candidate not found.";
            return null;
        }

        message = "Candidate found successfully.";
        return candidate;
    }

        public IEnumerable<Candidate> GetAllCandidates()
    {
        return _CandidateDataStore.GetAll();
    }






}
