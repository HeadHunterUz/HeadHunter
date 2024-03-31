using HeadHunter.Domain.Commons;
using HeadHunter.Domain.Entities.Jobs;
using HeadHunter.Domain.Entities.Users;

namespace HeadHunter.Domain.Entities.Vacancies;

public class BasketVacancy : Auditable
{
    public long VacancyId { get; set; }
    public long UserId { get; set; }
}