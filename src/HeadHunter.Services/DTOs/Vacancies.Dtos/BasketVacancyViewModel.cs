using HeadHunter.Services.DTOs.Jobs.Dtos;

namespace HeadHunter.Services.DTOs.Vacancies.Dtos;
public record BasketVacancyViewModel(
    long Id,
    JobVacancyViewModel JobVacancy,
    long UserViewModel User);
