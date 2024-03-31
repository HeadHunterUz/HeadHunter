using HeadHunter.Domain.Commons;
using HeadHunter.Domain.Entities.Industries;

namespace HeadHunter.Domain.Entities.Core;

public class Company : Auditable
{
    public string Name { get; set; }
    public long IndustryId { get; set; }
    public string Details { get; set; }
    public long AddressId { get; set; }
}