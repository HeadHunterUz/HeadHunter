using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities;
public class Address:Auditable
{
    public long Id { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
}
