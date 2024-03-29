using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities.Core;

public class Resume : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
    public string JobTitle { get; set; }
    public string Education { get; set; }
}
