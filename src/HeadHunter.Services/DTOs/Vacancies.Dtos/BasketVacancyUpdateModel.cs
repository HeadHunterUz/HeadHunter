namespace HeadHunter.Services.DTOs.Vacancies.Dtos;
public record BasketVacancyUpdateModel(
    long Id,
    long VacancyId,
    long UserId);
