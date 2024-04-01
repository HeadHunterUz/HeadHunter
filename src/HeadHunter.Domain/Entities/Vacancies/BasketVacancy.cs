using HeadHunter.Domain.Commons;
using HeadHunter.Domain.Entities.Jobs;
using HeadHunter.Domain.Entities.Users;

namespace HeadHunter.Domain.Entities.Vacancies;

public class BasketVacancy : Auditable
{
    /// <summary>
    /// Basket VacancyId
    /// </summary>
    public long JobVacancyId { get; set; }
    /// <summary>
    /// UserId
    /// </summary>
    public long UserId { get; set; }
}