namespace HeadHunter.Domain.Entities;
public class IndustryCategories
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long ParentId { get; set; }
}
