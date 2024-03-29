using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities;

public class Industry : Auditable
{ 
    public string Name { get; set; }
    public long CategoryId {  get; set; }
    public IndustryCategories industryCategories { get; set; }
}
