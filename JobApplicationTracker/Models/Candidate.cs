using System;

namespace JobApplicationTracker.Models;
//sure from email
public class Candidate:IEntity
{
    public int Id { get; set; }
    private string? fullname;
    public string Email { get; set; }=""; //valid and unique
    public string Phone { get; set; }="";
    private int yearsOfExperience;
    public string? FullName{
            get { return fullname; }
            set { if(string.IsNullOrEmpty(value)) throw new ArgumentException("Full name cannot be null or empty"); fullname = value; }
        }

    public int YearsOfExperience{
            get { return yearsOfExperience; }
            set { if(value < 0) throw new ArgumentException("Years of experience must be 0 or more"); yearsOfExperience = value; }
        }    
}
