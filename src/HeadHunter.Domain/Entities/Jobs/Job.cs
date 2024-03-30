using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities.Jobs;

public class Job : Auditable
{
    public string Name { get; set; }
}