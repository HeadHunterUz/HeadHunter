namespace HeadHunter.Services.DTOs.Vacancies.Dtos.VacancySkills;
public record VacancySkillUpdateModel(
    long Id, string Name,
    long VacancyId);