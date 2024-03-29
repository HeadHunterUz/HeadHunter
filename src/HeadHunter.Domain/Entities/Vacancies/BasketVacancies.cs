using HeadHunter.Domain.Commons;
using HeadHunter.Domain.Entities.Jobs;
using HeadHunter.Domain.Entities.Users;

namespace HeadHunter.Domain.Entities.Vacancies;

public class BasketVacancies : Auditable
{
    public long VacancyId { get; set; }
    public JobVacancy jobVacancy { get; set; }
    public long UserId { get; set; }
    public User user { get; set; }
}