namespace HeadHunter.Services.DTOs.Vacancies.Dtos.BasketVacancies;
public record BasketVacancyUpdateModel(
    long Id,
    long VacancyId,
    long UserId);