using HeadHunter.Domain.Commons;
using HeadHunter.Domain.Entities.Jobs;
using HeadHunter.Domain.Entities.Users;
using HeadHunter.Domain.Enums;

namespace HeadHunter.Domain.Entities.Core;

public class Application : Auditable
{
    /// <summary>
    /// user's Id for application
    /// </summary>
    public long UserId { get; set; }
    /// <summary>
    /// Apply Status , default=pending
    /// </summary>
    public ApplyStatus ApplyStatus { get; set; } = ApplyStatus.Pending;
    /// <summary>
    /// VacancyId for application
    /// </summary>
    public long VacancyId { get; set; }
    /// <summary>
    /// CompanyId for application
    /// </summary>
    public long CompanyId { get; set; }
}