using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities.Jobs;

public class Job : Auditable
{
    /// <summary>
    /// Job's Name
    /// </summary>
    public string Name { get; set; }
}