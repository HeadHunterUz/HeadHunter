using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities;
public class VacancySkills:Auditable
{
    public long Id {  get; set; }
    public string Name { get; set; }
    public long VacancyId {  get; set; }
    public JobVacancies jobVacancies {  get; set; } 
}
