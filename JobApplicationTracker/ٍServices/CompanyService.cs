using System;
using JobApplicationTracker.Models;
using JobApplicationTracker.Data;
namespace JobApplicationTracker.Services;

public class CompanyService
{
    private readonly DataStore<Company> _companyDataStore;
    private readonly Stack<(int CompanyId, CompanyStatus OldStatus)> oldstate = new();
    public CompanyService(DataStore<Company> companyDataStore)
    {
        _companyDataStore = companyDataStore;
    }
    public bool AddCompany(Company company)
    {
        if (company == null)    
        return false;
            _companyDataStore.Add(company);
            Console.WriteLine($"Company '{company.Name}' added successfully.");
            return true;
        
    }

    public bool UpdateCompany(Company company)
    {
        if (_companyDataStore.GetById(company.Id) == null)
        {
            Console.WriteLine("Company not found.");
            return false;
        }
        _companyDataStore.Update(company);
        Console.WriteLine($"Company '{company.Name}' updated successfully.");
        return true;
    }
    public bool RemoveCompany(int companyId)
    {
        if (_companyDataStore.GetById(companyId) == null)
        {
            Console.WriteLine("Company not found.");
            return false;
        }
        _companyDataStore.Remove(companyId);
        Console.WriteLine($"Company with ID {companyId} removed successfully.");
        return true;
    }


    public Company? GetById(int id)
    {
        if (_companyDataStore.GetById(id) == null)
        {
            Console.WriteLine("Company not found.");
            return null;
        }
        return _companyDataStore.GetById(id);
    }

    public IEnumerable<Company>? GetAll()
    {
        var companies = _companyDataStore.GetAll();
        if (companies == null)
        {
            Console.WriteLine("No companies found.");
            return null;
        }
        return companies;
    }

    public void ChangeCompanyStatus(int companyId, CompanyStatus newStatus)
    {
        var company = _companyDataStore.GetById(companyId);
        if (company == null)
        {
            Console.WriteLine("Company not found.");
            return;
        }
        if (company.Status == newStatus)
        {
            Console.WriteLine($"Company '{company.Name}' is already in the status '{newStatus}'.");
            return;
        }

        oldstate.Push((companyId, company.Status));
        company.Status = newStatus;
      _companyDataStore.Update(company);
      
    }

     public List<Company>? Getbystatus(CompanyStatus status)
    {
        var companies = _companyDataStore.GetAll();
        if (companies == null)
        {
            Console.WriteLine("No companies found.");
            return null;
        }
        var CompanieswithStatu = new List<Company>();
        foreach (var company in companies)
        {
            if (company.Status == status)
            {
                CompanieswithStatu.Add(company);
            }
        }
        return CompanieswithStatu;
    }

    public void UndoStatus()
    {
        if (oldstate.Count == 0)
        {
            Console.WriteLine("No status changes to undo.");
            return;
        }


         var (companyId, oldStatus) = oldstate.Pop();
        var company = _companyDataStore.GetById(companyId);
        if (company == null)
        {
            Console.WriteLine("Company not found.");
            return;
        }
        company.Status = oldStatus;
        _companyDataStore.Update(company);
        Console.WriteLine($"Reverted status of company '{company.Name}' to '{oldStatus}'.");
    }




}

