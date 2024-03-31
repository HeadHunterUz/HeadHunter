using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities.Industries;

public class IndustryCategory : Auditable
{
    /// <summary>
    /// IndustryCategory's Name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Parent Id
    /// </summary>
    public long ParentId { get; set; }
}