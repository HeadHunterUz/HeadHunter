using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;

namespace HeadHunter.Services.DTOs.Vacancies.Dtos.VacancySkills;
public class VacancySkillViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long JobVacancyId { get; set; }
}