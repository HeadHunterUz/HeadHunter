using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;

namespace HeadHunter.Services.DTOs.Vacancies.Dtos.VacancySkills;
public record VacancySkillViewModel(
    long Id,
    string Name,
    JobVacancyViewModel Job);