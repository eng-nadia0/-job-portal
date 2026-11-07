

using JobApplicationTracker.Data;
using JobApplicationTracker.Models;
using JobApplicationTracker.Services;

namespace JobApplicationTracker;

internal class Program
{
    static void Main(string[] args)
    {
        // Data Stores
        var companyStore = new DataStore<Company>();
        var recruiterStore = new DataStore<Recruiter>();
        var jobStore = new DataStore<Job>();
        var candidateStore = new DataStore<Candidate>();
        var applicationStore = new DataStore<Application>();

        // Email HashSet
        var emails = new HashSet<string>(
            StringComparer.OrdinalIgnoreCase);

        // Services
        var companyService = new CompanyService(companyStore);

        var recruiterService =
            new RecruiterService(recruiterStore, companyService);

        var jobService =
            new JobService(jobStore, recruiterService, companyService);

        var candidateService =
            new CandidateService(candidateStore, emails);

        var applicationService =
            new ApplicationService(
                applicationStore,
                jobStore,
                candidateStore);

        // Seed Data
        SeedData(
            companyService,
            recruiterService,
            jobService,
            candidateService);

        // Main Menu
        bool running = true;

        while (running)
        {
            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("     JOB APPLICATION TRACKER");
            Console.WriteLine("================================");
            Console.WriteLine("1. Manage Companies");
            Console.WriteLine("2. Manage Recruiters");
            Console.WriteLine("3. Manage Jobs");
            Console.WriteLine("4. Manage Candidates");
            Console.WriteLine("5. Manage Applications");
            Console.WriteLine("0. Exit");

            Console.Write("\nChoose an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    CompanyMenu(companyService);
                    break;

                case "2":
                    RecruiterMenu(recruiterService);
                    break;

                case "3":
                    JobMenu(jobService);
                    break;

                case "4":
                    CandidateMenu(candidateService);
                    break;

                case "5":
                    ApplicationMenu(applicationService);
                    break;

                case "0":
                    running = false;
                    Console.WriteLine("...Existing");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }
    }

    // ================= SEED DATA =================

    static void SeedData(
        CompanyService companies,
        RecruiterService recruiters,
        JobService jobs,
        CandidateService candidates)
    {
        // Companies
        companies.AddCompany(new Company
        {
            Name = "Vodafone",
            Industry = "Telecom",
            City = "Cairo",
            Status = CompanyStatus.Approved
        });

        companies.AddCompany(new Company
        {
            Name = "Valeo",
            Industry = "Software",
            City = "Giza",
            Status = CompanyStatus.Approved
        });

        companies.AddCompany(new Company
        {
            Name = "NewCo",
            Industry = "Retail",
            City = "Alex",
            Status = CompanyStatus.Pending
        });

        // Recruiters
        recruiters.AddRecruiter(
            new Recruiter
            {
                Name = "Omar",
                Email = "omar@vodafone.com",
                CompanyId = 1
            },
            out string message);

        recruiters.AddRecruiter(
            new Recruiter
            {
                Name = "Nour",
                Email = "nour@valeo.com",
                CompanyId = 2
            },
            out message);

        recruiters.AddRecruiter(
            new Recruiter
            {
                Name = "Hana",
                Email = "hana@newco.com",
                CompanyId = 3
            },
            out message);

        // Jobs
        jobs.AddJob(new Job
        {
            Title = "Junior C# Developer",
            Description = "Backend work",
            Salary = 15000,
            Location = "Cairo",
            IsOpen = true,
            DatePosted = DateTime.Now.AddDays(-10),
            CompanyId = 1,
            RecruiterId = 1
        }, out message);

        jobs.AddJob(new Job
        {
            Title = "QA Engineer",
            Description = "Testing",
            Salary = 12000,
            Location = "Cairo",
            IsOpen = true,
            DatePosted = DateTime.Now.AddDays(-8),
            CompanyId = 1,
            RecruiterId = 1
        }, out message);

        jobs.AddJob(new Job
        {
            Title = "Support Agent",
            Description = "Customer support",
            Salary = 8000,
            Location = "Cairo",
            IsOpen = false,
            DatePosted = DateTime.Now.AddDays(-30),
            CompanyId = 1,
            RecruiterId = 1
        }, out message);

        jobs.AddJob(new Job
        {
            Title = ".NET Developer",
            Description = "APIs",
            Salary = 20000,
            Location = "Giza",
            IsOpen = true,
            DatePosted = DateTime.Now.AddDays(-5),
            CompanyId = 2,
            RecruiterId = 2
        }, out message);

        jobs.AddJob(new Job
        {
            Title = "UI Designer",
            Description = "Design",
            Salary = 14000,
            Location = "Giza",
            IsOpen = true,
            DatePosted = DateTime.Now.AddDays(-3),
            CompanyId = 2,
            RecruiterId = 2
        }, out message);

        // Candidate Seed Data
        AddSeedCandidate(candidates, "Mayar Ahmed",
            "mayar@mail.com", "01000000001", 1);

        AddSeedCandidate(candidates, "Sara Ali",
            "sara@mail.com", "01000000002", 0);

        AddSeedCandidate(candidates, "Ali Hassan",
            "ali@mail.com", "01000000003", 3);

        AddSeedCandidate(candidates, "Nada Samy",
            "nada@mail.com", "01000000004", 2);

        AddSeedCandidate(candidates, "Omar Khaled",
            "omarkh@mail.com", "01000000005", 5);
    }

    static void AddSeedCandidate(
        CandidateService service,
        string name,
        string email,
        string phone,
        int experience)
    {
        var candidate = new Candidate
        {
            FullName = name,
            Email = email,
            Phone = phone,
            YearsOfExperience = experience
        };

        service.AddCandidate(candidate);
    }

    // ================= COMPANY MENU =================

    static void CompanyMenu(CompanyService service)
    {
        bool back = false;

        while (!back)
        {
            Console.Clear();

            Console.WriteLine("===== COMPANY MENU =====");
            Console.WriteLine("1. Add Company");
            Console.WriteLine("2. Show All Companies");
            Console.WriteLine("3. Find Company By ID");
            Console.WriteLine("4. Update Company");
            Console.WriteLine("5. Remove Company");
            Console.WriteLine("0. Back");
            Console.Write("Choose: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Company name: ");
                    string name = Console.ReadLine() ?? "";

                    Console.Write("Industry: ");
                    string industry = Console.ReadLine() ?? "";

                    Console.Write("City: ");
                    string city = Console.ReadLine() ?? "";

                    service.AddCompany(new Company
                    {
                        Name = name,
                        Industry = industry,
                        City = city,
                        Status = CompanyStatus.Pending
                    });

                    Console.WriteLine("Company added.");
                    Pause();
                    break;

                case "2":
                    ShowCompanies(service);
                    break;

                case "3":
                    Console.Write("Company ID: ");

                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        var company1 = service.GetById(id);

                        if (company1 == null)
                            Console.WriteLine("Company not found.");
                        else
                            PrintCompany(company1);
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID.");
                    }

                    Pause();
                    break;

                case "4":
                    Console.Write("Company ID: ");

                    if (!int.TryParse(Console.ReadLine(), out int companyId1))
                    {
                        Console.WriteLine("Invalid company ID.");
                        Pause();
                        break;
                    }

                    var company = service.GetById(companyId1);

                    if (company == null)
                    {
                        Console.WriteLine("Company not found.");
                        Pause();
                        break;
                    }

                    Console.Write("Company name: ");
                    company.Name = Console.ReadLine() ?? "";

                    Console.Write("Industry: ");
                    company.Industry = Console.ReadLine() ?? "";

                    Console.Write("City: ");
                    company.City = Console.ReadLine() ?? "";

                    Console.WriteLine("Select Status:");
                    Console.WriteLine("1. Pending");
                    Console.WriteLine("2. Approved");
                    Console.WriteLine("3. Rejected");

                    string statusChoice = Console.ReadLine() ?? "";

                    switch (statusChoice)
                    {
                        case "1":
                            company.Status = CompanyStatus.Pending;
                            break;
                        case "2":
                            company.Status = CompanyStatus.Approved;
                            break;
                        case "3":
                            company.Status = CompanyStatus.Rejected;
                            break;
                        default:
                            Console.WriteLine("Invalid status. Keeping the current status.");
                            break;
                    }

                    if (service.UpdateCompany(company))
                        Console.WriteLine("Company updated successfully.");
                    else
                        Console.WriteLine("Update failed.");

                    Pause();
                    break;
                case "5":
                    Console.Write("Company ID: ");

                    if (!int.TryParse(Console.ReadLine(), out int companyId))
                    {
                        Console.WriteLine("Invalid company ID.");
                        Pause();
                        break;
                    }

                    bool removed = service.RemoveCompany(companyId);

                    if (removed)
                    {
                        Console.WriteLine("Company has been removed successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Removing failed. Company may not exist.");
                    }

                    Pause();
                    break;

                case "0":
                    back = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }
    }






    static void ShowCompanies(CompanyService service)
    {
        var companies = service.GetAll();
        if (companies == null)
        {
            Console.WriteLine("There are no companies");
            return;
        }
        Console.WriteLine("\n===== COMPANIES =====");

        foreach (var company in companies)
            PrintCompany(company);

        Pause();
    }

    static void PrintCompany(Company company)
    {
        Console.WriteLine(
            $"ID: {company.Id} | Name: {company.Name} | Industry: {company.Industry} | City: {company.City} | Status: {company.Status}");
    }

    // ================= RECRUITER MENU =================

    static void RecruiterMenu(RecruiterService service)
    {
        bool back = false;

        while (!back)
        {
            Console.Clear();

            Console.WriteLine("===== RECRUITER MENU =====");
            Console.WriteLine("1. Add Recruiter");
            Console.WriteLine("2. Show All Recruiters");
            Console.WriteLine("3. Find Recruiter By ID");
            Console.WriteLine("4. Remove Recruiter");
            Console.WriteLine("0. Back");
            Console.Write("Choose: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Name: ");
                    string name = Console.ReadLine() ?? "";

                    Console.Write("Email: ");
                    string email = Console.ReadLine() ?? "";

                    Console.Write("Company ID: ");

                    if (!int.TryParse(Console.ReadLine(), out int companyId))
                    {
                        Console.WriteLine("Invalid company ID.");
                        Pause();
                        break;
                    }

                    var recruiter = new Recruiter
                    {
                        Name = name,
                        Email = email,
                        CompanyId = companyId
                    };

                    service.AddRecruiter(recruiter, out string addMessage);
                    Console.WriteLine(addMessage);
                    Pause();
                    break;

                case "2":
                    foreach (var item in service.GetAllRecruiters())
                    {
                        Console.WriteLine(
                            $"ID: {item.Id} | Name: {item.Name} | Email: {item.Email} | Company ID: {item.CompanyId}");
                    }

                    Pause();
                    break;

                case "3":
                    Console.Write("Recruiter ID: ");

                    if (int.TryParse(Console.ReadLine(), out int recruiterId))
                    {
                        var item = service.GetRecruiterById(
                            recruiterId, out string message);

                        Console.WriteLine(message);

                        if (item != null)
                        {
                            Console.WriteLine(
                                $"{item.Id} | {item.Name} | {item.Email} | Company: {item.CompanyId}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID.");
                    }

                    Pause();
                    break;

              
                case "4":
                    Console.Write("Recruiter ID: ");

                    if (int.TryParse(Console.ReadLine(), out int removeId))
                    {
                        service.RemoveRecruiter(removeId, out string removeMessage);
                        Console.WriteLine(removeMessage);
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID.");
                    }

                    Pause();
                    break;

                case "0":
                    back = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }
    }

    // ================= JOB MENU =================

    static void JobMenu(JobService service)
    {
        bool back = false;

        while (!back)
        {
            Console.Clear();

            Console.WriteLine("===== JOB MENU =====");
            Console.WriteLine("1. Add Job");
            Console.WriteLine("2. Find Job By ID");
            Console.WriteLine("0. Back");
            Console.Write("Choose: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Job title: ");
                    string title = Console.ReadLine() ?? "";

                    Console.Write("Description: ");
                    string description = Console.ReadLine() ?? "";

                    Console.Write("Salary: ");

                    if (!decimal.TryParse(Console.ReadLine(), out decimal salary))
                    {
                        Console.WriteLine("Invalid salary.");
                        Pause();
                        break;
                    }

                    Console.Write("Location: ");
                    string location = Console.ReadLine() ?? "";

                    Console.Write("Company ID: ");

                    if (!int.TryParse(Console.ReadLine(), out int companyId))
                    {
                        Console.WriteLine("Invalid company ID.");
                        Pause();
                        break;
                    }

                    Console.Write("Recruiter ID: ");

                    if (!int.TryParse(Console.ReadLine(), out int recruiterId))
                    {
                        Console.WriteLine("Invalid recruiter ID.");
                        Pause();
                        break;
                    }

                    var job = new Job
                    {
                        Title = title,
                        Description = description,
                        Salary = salary,
                        Location = location,
                        IsOpen = true,
                        DatePosted = DateTime.Now,
                        CompanyId = companyId,
                        RecruiterId = recruiterId
                    };

                    service.AddJob(job, out string addMessage);
                    Console.WriteLine(addMessage);
                    Pause();
                    break;

               
                

                case "2":
                    Console.Write("Recruiter ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int removeRecruiterId))
                    {
                        Console.WriteLine("Invalid recruiter ID.");
                        Pause();
                        break;
                    }

                    Console.Write("Job ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int jobId))
                    {
                        Console.WriteLine("Invalid job ID.");
                        Pause();
                        break;
                    }

                    service.RemoveJob(
                        removeRecruiterId,
                        jobId,
                        out string removeMessage);

                    Console.WriteLine(removeMessage);
                    Pause();
                    break;

                case "0":
                    back = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }
    }

    // ================= CANDIDATE MENU =================

    static void CandidateMenu(CandidateService service)
    {
        bool back = false;

        while (!back)
        {
            Console.Clear();

            Console.WriteLine("===== CANDIDATE MENU =====");
            Console.WriteLine("1. Add Candidate");
            Console.WriteLine("2. Update Candidate");
            Console.WriteLine("3. Remove Candidate");
            Console.WriteLine("4. Get Candidate By ID");
            Console.WriteLine("5. Get All Candidates");
            Console.WriteLine("0. Back");
            Console.Write("Choose: ");

            switch (Console.ReadLine())
            {
                case "1":
                    AddCandidateMenu(service);
                    break;

                case "2":
                    UpdateCandidateMenu(service);
                    break;

                case "3":
                    RemoveCandidateMenu(service);
                    break;

                case "4":
                    GetCandidateMenu(service);
                    break;

                case "5":
                    ShowAllCandidates(service);
                    break;

                case "0":
                    back = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }
    }

    static void AddCandidateMenu(CandidateService service)
    {
        Console.Write("Enter name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Enter email: ");
        string email = Console.ReadLine() ?? "";

        Console.Write("Enter phone: ");
        string phone = Console.ReadLine() ?? "";

        Console.Write("Enter years of experience: ");

        if (!int.TryParse(Console.ReadLine(), out int experience))
        {
            Console.WriteLine("Invalid experience.");
            Pause();
            return;
        }

        var candidate = new Candidate
        {
            FullName = name,
            Email = email,
            Phone = phone,
            YearsOfExperience = experience
        };

        service.AddCandidate(candidate);
        Pause();
    }

    static void UpdateCandidateMenu(CandidateService service)
    {
        Console.Write("Enter candidate ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            Pause();
            return;
        }

        var candidate = service.GetCandidateById(id, out string message);

        if (candidate == null)
        {
            Console.WriteLine(message);
            Pause();
            return;
        }

        Console.Write("New name: ");
        candidate.FullName = Console.ReadLine() ?? "";

        Console.Write("New email: ");
        candidate.Email = Console.ReadLine() ?? "";

        Console.Write("New phone: ");
        candidate.Phone = Console.ReadLine() ?? "";

        Console.Write("New years of experience: ");

        if (!int.TryParse(Console.ReadLine(), out int experience))
        {
            Console.WriteLine("Invalid experience.");
            Pause();
            return;
        }

        candidate.YearsOfExperience = experience;

        service.uodate(id, candidate, out message);

        Console.WriteLine(message);
        Pause();
    }

    static void RemoveCandidateMenu(CandidateService service)
    {
        Console.Write("Enter candidate ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            Pause();
            return;
        }

        service.RemoveCandidate(id, out string message);

        Console.WriteLine(message);
        Pause();
    }

    static void GetCandidateMenu(CandidateService service)
    {
        Console.Write("Enter candidate ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            Pause();
            return;
        }

        var candidate = service.GetCandidateById(id, out string message);

        Console.WriteLine(message);

        if (candidate != null)
            PrintCandidate(candidate);

        Pause();
    }

    static void ShowAllCandidates(CandidateService service)
    {
        Console.WriteLine("\n===== ALL CANDIDATES =====");

        foreach (var candidate in service.GetAllCandidates())
            PrintCandidate(candidate);

        Pause();
    }

    static void PrintCandidate(Candidate candidate)
    {
        Console.WriteLine(
            $"ID: {candidate.Id} | Name: {candidate.FullName} | Email: {candidate.Email} | Phone: {candidate.Phone} | Experience: {candidate.YearsOfExperience}");
    }

    // ================= APPLICATION MENU =================

    static void ApplicationMenu(ApplicationService service)
    {
        bool back = false;

        while (!back)
        {
            Console.Clear();

            Console.WriteLine("===== APPLICATION MENU =====");
            Console.WriteLine("1. Apply for Job");
            Console.WriteLine("2. Show All Applications");
            Console.WriteLine("3. Find Application By ID");
            Console.WriteLine("4. Show Applications By Candidate");
            Console.WriteLine("5. Show Applications By Job");
            Console.WriteLine("0. Back");
            Console.Write("Choose: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Job ID: ");

                    if (!int.TryParse(Console.ReadLine(), out int jobId))
                    {
                        Console.WriteLine("Invalid job ID.");
                        Pause();
                        break;
                    }

                    Console.Write("Candidate ID: ");

                    if (!int.TryParse(Console.ReadLine(), out int candidateId))
                    {
                        Console.WriteLine("Invalid candidate ID.");
                        Pause();
                        break;
                    }


                    var application = new Application
                    {
                        JobId = jobId,
                        CandidateId = candidateId
                    };

                    service.AddApplication(application, out string addMessage);

                    Console.WriteLine(addMessage);
                    Pause();
                    break;

                case "2":
                    foreach (var item in service.GetAllApplications())
                        PrintApplication(item);

                    Pause();
                    break;

                case "3":
                    Console.Write("Application ID: ");

                    if (!int.TryParse(Console.ReadLine(), out int applicationId))
                    {
                        Console.WriteLine("Invalid ID.");
                        Pause();
                        break;
                    }

                    var found = service.GetApplicationById(
                        applicationId, out string message);

                    Console.WriteLine(message);

                    if (found != null)
                        PrintApplication(found);

                    Pause();
                    break;

                case "4":
                    Console.Write("Candidate ID: ");

                    if (!int.TryParse(Console.ReadLine(), out int candidateSearchId))
                    {
                        Console.WriteLine("Invalid ID.");
                        Pause();
                        break;
                    }

                    foreach (var item in service.GetApplicationsByCandidate(candidateSearchId))
                        PrintApplication(item);

                    Pause();
                    break;

                case "5":
                    Console.Write("Job ID: ");

                    if (!int.TryParse(Console.ReadLine(), out int jobSearchId))
                    {
                        Console.WriteLine("Invalid ID.");
                        Pause();
                        break;
                    }

                    foreach (var item in service.GetApplicationsByJob(jobSearchId))
                        PrintApplication(item);

                    Pause();
                    break;

                case "0":
                    back = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }
    }

    static void PrintApplication(Application application)
    {
        Console.WriteLine($"Application ID: {application.Id} | Job ID: {application.JobId} | Candidate ID: {application.CandidateId} | AppliedDate:A {application.AppliedDate} | ApplicationStatus: {application.Status}");
    }


    static void Pause()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}
