using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities;
public class Address:Auditable
{
    public string Country { get; set; }
    public string City { get; set; }
}
