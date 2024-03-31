using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities.Core;

public class Experience : Auditable
{
    public string Country { get; set; }
    public string City { get; set; }
}