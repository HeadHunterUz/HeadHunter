using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities;

public class UserSkills : Auditable
{
    public long UserId { get; set; }
    public string Name { get; set; }
}