using HeadHunter.Domain.Commons;
using HeadHunter.Domain.Entities.Users;

namespace HeadHunter.Domain.Entities.Core;

public class Experience : Auditable
{
    /// <summary>
    /// User's Id for experience 
    /// </summary>
    public long UserId { get; set; }
    /// <summary>
    /// Company's Id
    /// </summary>
    public long CompanyId { get; set; }
    /// <summary>
    /// JobTitle
    /// </summary>
    public string JobTitle { get; set; }
    /// <summary>
    /// Job position for experience
    /// </summary>
    public string Position { get; set; }
    /// <summary>
    /// Start Time
    /// </summary>
    public DateTime StartTime { get; set; }
    /// <summary>
    /// End Time 
    /// </summary>
    public DateTime? EndTime { get; set; }
}