using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities.Industries;

public class IndustryCategory : Auditable
{
    public string Name { get; set; }
    public long ParentId { get; set; }
}