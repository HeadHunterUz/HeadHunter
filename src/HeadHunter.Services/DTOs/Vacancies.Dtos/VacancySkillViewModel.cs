using HeadHunter.Services.DTOs.Jobs.Dtos;

namespace HeadHunter.Services.DTOs.Vacancies.Dtos;
public record VacancySkillViewModel(
    long Id, 
    string Name, 
    JobVacancyViewModel Job);
