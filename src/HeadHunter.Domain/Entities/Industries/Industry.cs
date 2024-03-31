using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities.Industries;

public class Industry : Auditable
{
    /// <summary>
    /// Industry's Name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Industry's CategoryId
    /// </summary>
    public long CategoryId { get; set; }
}