using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public long IndustryId { get; set; }
    public Industry Industry { get; set; }
    public long ResumeId { get; set; }
    public Resume Resume { get; set; }
    public string Photo { get; set; }
    public long AddressId { get; set; }
    public Address Address { get; set; }
    public int AppliedJobsCount { get; set; }

}

