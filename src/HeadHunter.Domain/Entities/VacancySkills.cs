using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities;
public class VacancySkill : Auditable
{
    public string Name { get; set; }
    public long VacancyId {  get; set; }
    public JobVacancies jobVacancies {  get; set; } 
}
