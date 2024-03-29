namespace HeadHunter.Domain.Entities;
public class VacancySkills
{
    public long Id {  get; set; }
    public string Name { get; set; }
    public long VacancyId {  get; set; }
    public JobVacanciesId jobVacanciesId {  get; set; } 
}
