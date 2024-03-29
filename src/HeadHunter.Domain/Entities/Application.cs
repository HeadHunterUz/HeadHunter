using HeadHunter.Domain.Commons;
using HeadHunter.Domain.Enums;
namespace HeadHunter.Domain.Entities;
public class Application:Auditable
{
    public long UserId { get; set; }
    public User user { get; set; }
    public ApplyStatus applyStatus { get; set; }
    public long VacancyId { get; set; }
    public JobVacancy jobVacancy { get; set; }
    public long CompanyId { get; set; }
    public Company company { get; set; }
}
