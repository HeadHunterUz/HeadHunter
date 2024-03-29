using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities;
public class Jobs:Auditable
{
    public long Id { get; set; }
    public string Name { get; set; }
}
