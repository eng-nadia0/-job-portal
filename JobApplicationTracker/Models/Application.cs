using System;

namespace JobApplicationTracker.Models;

public class Application : IEntity
{
    public int Id { get; set; }
    public int JobId { get; set; }
    public int CandidateId { get; set; }
    public DateTime AppliedDate { get; set; } = DateTime.Now;
    public ApplicationStatus Status { get; set; }= ApplicationStatus.Submitted;
}

public enum ApplicationStatus
{
    Submitted,
    UnderReview,
    Interview,
    Accepted,
    Rejected
}
