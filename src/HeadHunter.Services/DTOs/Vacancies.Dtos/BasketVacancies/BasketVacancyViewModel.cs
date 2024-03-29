using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;
using HeadHunter.Services.DTOs.Users.Dtos;

namespace HeadHunter.Services.DTOs.Vacancies.Dtos.BasketVacancies;

public record BasketVacancyViewModel(
    long Id,
    long vacancyId,
    JobVacancyViewModel JobVacancy,
    long UserId,
    UserViewModel User);