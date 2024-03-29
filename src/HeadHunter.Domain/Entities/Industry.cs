using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities;

public class Industry : Auditable
{ 
    public long Id { get; set; }
    public string Name { get; set; }
    public long CategoryId {  get; set; }
    public IndustryCategories industryCategories { get; set; }
}
