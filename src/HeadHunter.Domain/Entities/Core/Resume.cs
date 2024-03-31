using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities.Core;

public class Resume : Auditable
{
    /// <summary>
    /// User's Id to fill Resume
    /// </summary>
    public long UserId { get; set; }
    /// <summary>
    /// User's FirstName
    /// </summary>
    public string FirstName { get; set; }
    /// <summary>
    /// User's Last Name
    /// </summary>
    public string LastName { get; set; }
    /// <summary>
    /// Description about user
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// Job title
    /// </summary>
    public string JobTitle { get; set; }
    /// <summary>
    /// Education
    /// </summary>
    public string Education { get; set; }
}