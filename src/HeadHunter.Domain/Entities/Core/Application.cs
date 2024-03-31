using HeadHunter.Domain.Commons;
using HeadHunter.Domain.Entities.Jobs;
using HeadHunter.Domain.Entities.Users;
using HeadHunter.Domain.Enums;

namespace HeadHunter.Domain.Entities.Core;

public class Application : Auditable
{
    public long UserId { get; set; }
    public ApplyStatus ApplyStatus { get; set; } = ApplyStatus.Pending;
    public long VacancyId { get; set; }
    public long CompanyId { get; set; }
}