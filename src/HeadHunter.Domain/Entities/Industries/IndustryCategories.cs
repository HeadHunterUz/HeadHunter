using HeadHunter.Domain.Commons;
namespace HeadHunter.Domain.Entities.Industries;
public class IndustryCategories : Auditable
{
    public string Name { get; set; }
    public long ParentId { get; set; }
}
