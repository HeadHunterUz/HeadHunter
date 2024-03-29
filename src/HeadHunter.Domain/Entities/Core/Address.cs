using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities.Core;

public class Address : Auditable
{
    public string Country { get; set; }
    public string City { get; set; }
}