using System;

namespace JobApplicationTracker.Models;

public class Job:IEntity
{
  public int Id { get; set; }
  private string? title ;
  public string Description { get; set; }="";
  private decimal salary;
  public string Location { get; set; }="";
  public bool IsOpen { get; set; }= true;
  public DateTime DatePosted { get; set; }= DateTime.Now;
  public int CompanyId { get; set; }
  public int RecruiterId { get; set; }
   public string? Title{
        get { return title; }
        set { if(string.IsNullOrEmpty(value)) throw new ArgumentException("Title cannot be null or empty"); title = value; }
    }

    public decimal Salary{
        get { return salary; }
        set { if(value <= 0) throw new ArgumentException("Salary cannot be zero or negative"); salary = value; }
    }
    


}


