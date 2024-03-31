using HeadHunter.Domain.Commons;
using HeadHunter.Domain.Entities.Core;
using HeadHunter.Domain.Entities.Industries;

namespace HeadHunter.Domain.Entities.Users;

public class User : Auditable
{
    /// <summary>
    /// user's First Name
    /// </summary>
    public string FirstName { get; set; }
    /// <summary>
    /// user's LastName
    /// </summary>
    public string LastName { get; set; }
    /// <summary>
    /// user's Phone Number
    /// </summary>
    public string Phone { get; set; }
    /// <summary>
    /// User's Email
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// user's Password
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// user's IndustryId
    /// </summary>
    public long IndustryId { get; set; }
    /// <summary>
    /// user's photo
    /// </summary>
    public string Photo { get; set; }
    /// <summary>
    /// user's Address
    /// </summary>
    public long AddressId { get; set; }
    /// <summary>
    /// user's applied job count
    /// </summary>
    public int AppliedJobsCount { get; set; }
}