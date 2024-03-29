using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities;

public class Experience : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long CompanyId { get; set; }
    public Company Company { get; set; }
    public string JobTitle { get; set; }
    public string Position { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}
