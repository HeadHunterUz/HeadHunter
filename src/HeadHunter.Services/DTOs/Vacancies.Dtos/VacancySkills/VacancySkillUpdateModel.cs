namespace HeadHunter.Services.DTOs.Vacancies.Dtos.VacancySkills;
public class VacancySkillUpdateModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long VacancyId { get; set; }
}