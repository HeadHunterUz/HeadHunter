using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities.Vacancies;
public class VacancySkill : Auditable
{
    public string Name { get; set; }
    public long VacancyId { get; set; }
    public JobVacancies jobVacancies { get; set; }
}
