using HeadHunter.Domain.Commons;
using HeadHunter.Domain.Entities.Core;
using HeadHunter.Domain.Entities.Industries;

namespace HeadHunter.Domain.Entities.Users;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public long IndustryId { get; set; }
    public Industry Industry { get; set; }
    public string Photo { get; set; }
    public long AddressId { get; set; }
    public Address Address { get; set; }
    public int AppliedJobsCount { get; set; }
}