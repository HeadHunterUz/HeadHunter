using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities;
public class IndustryCategories : Auditable
{
    public string Name { get; set; }
    public long ParentId { get; set; }
}
