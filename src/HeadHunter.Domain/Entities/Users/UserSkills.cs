using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities.Users;

public class UserSkills : Auditable
{
    /// <summary>
    /// User's Id
    /// </summary>
    public long UserId { get; set; }
    /// <summary>
    /// UsersSkill's Name
    /// </summary>
    public string Name { get; set; }
}