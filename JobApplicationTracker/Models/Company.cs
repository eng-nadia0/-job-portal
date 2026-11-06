using System;

namespace JobApplicationTracker.Models;

// make Id is unique => done in DataStore.cs
public class Company : IEntity
{
    public int Id { get; set; }
    private string name="";
    public string Industry { get; set; }="";
    public string City { get; set; }="";
    public CompanyStatus Status { get; set; } = CompanyStatus.Pending;

    public string Name{
        get { return name; }
        set { if(string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name cannot be null or empty"); name = value; }
    }

}
public enum CompanyStatus
{
    Pending,
    Approved,
    Rejected
}

