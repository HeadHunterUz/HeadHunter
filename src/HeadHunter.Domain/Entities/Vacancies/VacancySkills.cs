using HeadHunter.Domain.Commons;
using HeadHunter.Domain.Entities.Jobs;

namespace HeadHunter.Domain.Entities.Vacancies;

public class VacancySkill : Auditable
{
    /// <summary>
    /// VacancySkill name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// VacancyId
    /// </summary>
    public long VacancyId { get; set; }
}