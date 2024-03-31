namespace HeadHunter.Services.DTOs.Vacancies.Dtos.BasketVacancies;
public class BasketVacancyUpdateModel
{
    public long Id { get; set; }
    public long JobVacancyId { get; set; }
    public long UserId { get; set; }
}