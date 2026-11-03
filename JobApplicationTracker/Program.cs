using System;
using System.Collections.Generic;
using JobApplicationTracker.Models;

public partial class Program
{
    private static void Main(string[] args)
    {
        List<Company> companies = new List<Company>();
        List<Recruiter> recruiters = new List<Recruiter>();
        List<Job> jobs = new List<Job>();
        List<Candidate> candidates = new List<Candidate>();

        SeedData(companies, recruiters, jobs, candidates);
        Menu();










        //=================menu=================
        void Menu()
        {
            while (true)
            {

                Console.WriteLine("Welcome to Job Application Tracker");
                Console.WriteLine("1. List all companies");
                Console.WriteLine("2. List all recruiters");
                Console.WriteLine("3. List all jobs");
                Console.WriteLine("4. List all candidates");
                Console.WriteLine("5. Exit");

                Console.Write("Enter your choice: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("choice cann't be null or empty. Please enter a number between 1 and 5.");
                    continue;
                }
                if (choice == 1)
                {
                    ListCompanies(companies);
                }
                else if (choice == 2)
                {
                    ListRecruiters(recruiters);
                }
                else if (choice == 3)
                {
                    ListJobs(jobs);
                }
                else if (choice == 4)
                {
                    ListCandidates(candidates);
                }
                else if (choice == 5)
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
        }





        //=================ShowData=================

        void ListCompanies(List<Company> companies)
        {
            Console.WriteLine("List of Companies:");
            foreach (var company in companies)
            {
                Console.WriteLine($"Id: {company.Id}, Name: {company.Name}, Industry: {company.Industry}, City: {company.City}, Status: {company.Status}");
            }
        }


        void ListRecruiters(List<Recruiter> recruiters)
        {
            Console.WriteLine("List of Recruiters:");
            foreach (var recruiter in recruiters)
            {
                Console.WriteLine($"Id: {recruiter.Id}, Name: {recruiter.Name}, Email: {recruiter.Email}, CompanyId: {recruiter.CompanyId}");
            }
        }

        void ListJobs(List<Job> jobs)
        {
            Console.WriteLine("List of Jobs:");
            foreach (var job in jobs)
            {
                Console.WriteLine($"Id: {job.Id}, Title: {job.Title}, Description: {job.Description}, Salary: {job.Salary}, Location: {job.Location}, IsOpen: {job.IsOpen}, DatePosted: {job.DatePosted}, CompanyId: {job.CompanyId}, RecruiterId: {job.RecruiterId}");
            }
        }
        void ListCandidates(List<Candidate> candidates)
        {
            Console.WriteLine("List of Candidates:");
            foreach (var candidate in candidates)
            {
                Console.WriteLine($"Id: {candidate.Id}, FullName: {candidate.FullName}, Email: {candidate.Email}, Phone: {candidate.Phone}, YearsOfExperience: {candidate.YearsOfExperience}");
            }
        }











        //===============Data=================
        //data from Ai 
        static void SeedData(List<Company> companies, List<Recruiter> recruiters,
                                 List<Job> jobs, List<Candidate> candidates)
        {
            // 3 companies
            companies.Add(new Company { Id = 1, Name = "Vodafone", Industry = "Telecom", City = "Cairo", Status = CompanyStatus.Approved });
            companies.Add(new Company { Id = 2, Name = "Valeo", Industry = "Software", City = "Giza", Status = CompanyStatus.Approved });
            companies.Add(new Company { Id = 3, Name = "NewCo", Industry = "Retail", City = "Alex", Status = CompanyStatus.Pending });

            // 3 recruiters
            recruiters.Add(new Recruiter { Id = 1, Name = "Omar", Email = "omar@vodafone.com", CompanyId = 1 });
            recruiters.Add(new Recruiter { Id = 2, Name = "Nour", Email = "nour@valeo.com", CompanyId = 2 });
            recruiters.Add(new Recruiter { Id = 3, Name = "Hana", Email = "hana@newco.com", CompanyId = 3 });

            // 6 jobs
            jobs.Add(new Job { Id = 1, Title = "Junior C# Developer", Description = "Backend work", Salary = 15000, Location = "Cairo", IsOpen = true, DatePosted = DateTime.Now.AddDays(-10), CompanyId = 1, RecruiterId = 1 });
            jobs.Add(new Job { Id = 2, Title = "QA Engineer", Description = "Testing", Salary = 12000, Location = "Cairo", IsOpen = true, DatePosted = DateTime.Now.AddDays(-8), CompanyId = 1, RecruiterId = 1 });
            jobs.Add(new Job { Id = 3, Title = "Support Agent", Description = "Customer support", Salary = 8000, Location = "Cairo", IsOpen = false, DatePosted = DateTime.Now.AddDays(-30), CompanyId = 1, RecruiterId = 1 });
            jobs.Add(new Job { Id = 4, Title = ".NET Developer", Description = "APIs", Salary = 20000, Location = "Giza", IsOpen = true, DatePosted = DateTime.Now.AddDays(-5), CompanyId = 2, RecruiterId = 2 });
            jobs.Add(new Job { Id = 5, Title = "UI Designer", Description = "Design", Salary = 14000, Location = "Giza", IsOpen = true, DatePosted = DateTime.Now.AddDays(-3), CompanyId = 2, RecruiterId = 2 });
            jobs.Add(new Job { Id = 6, Title = "Sales Associate", Description = "Retail sales", Salary = 9000, Location = "Alex", IsOpen = true, DatePosted = DateTime.Now.AddDays(-1), CompanyId = 3, RecruiterId = 3 });

            // 5 candidates
            candidates.Add(new Candidate { Id = 1, FullName = "Mayar Ahmed", Email = "mayar@mail.com", Phone = "01000000001", YearsOfExperience = 1 });
            candidates.Add(new Candidate { Id = 2, FullName = "Sara Ali", Email = "sara@mail.com", Phone = "01000000002", YearsOfExperience = 0 });
            candidates.Add(new Candidate { Id = 3, FullName = "Ali Hassan", Email = "ali@mail.com", Phone = "01000000003", YearsOfExperience = 3 });
            candidates.Add(new Candidate { Id = 4, FullName = "Nada Samy", Email = "nada@mail.com", Phone = "01000000004", YearsOfExperience = 2 });
            candidates.Add(new Candidate { Id = 5, FullName = "Omar Khaled", Email = "omarkh@mail.com", Phone = "01000000005", YearsOfExperience = 5 });
        }
    }
}