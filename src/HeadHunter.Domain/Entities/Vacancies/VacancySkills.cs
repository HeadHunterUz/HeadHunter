using HeadHunter.Domain.Commons;
using HeadHunter.Domain.Entities.Jobs;

namespace HeadHunter.Domain.Entities.Vacancies;

public class VacancySkill : Auditable
{
    public string Name { get; set; }
    public long VacancyId { get; set; }
    public JobVacancy jobVacancies { get; set; }
}
