using System;

namespace JobApplicationTracker.Models;
//sure from email
public class Recruiter:IEntity
{
    public int Id { get; set; }
    private string name="";
    public string Email { get; set; }="";
    public int CompanyId { get; set; }
    
        public string Name{
            get { return name; }
            set { if(string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name cannot be null or empty"); name = value; }
        }


}
