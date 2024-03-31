using HeadHunter.Domain.Commons;
using HeadHunter.Domain.Entities.Jobs;
using HeadHunter.Domain.Entities.Users;
using HeadHunter.Domain.Enums;

namespace HeadHunter.Domain.Entities.Core;

public class Application : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public ApplyStatus ApplyStatus { get; set; }
    public long VacancyId { get; set; }
    public JobVacancy JobVacancy { get; set; }
    public long CompanyId { get; set; }
    public Company Company { get; set; }
}