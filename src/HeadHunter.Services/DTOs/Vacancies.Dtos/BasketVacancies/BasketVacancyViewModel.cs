using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;
using HeadHunter.Services.DTOs.Users.Dtos;

namespace HeadHunter.Services.DTOs.Vacancies.Dtos.BasketVacancies;

public class BasketVacancyViewModel
{
    public long Id { get; set; }
    public long JobVacancyId { get; set; }
    public long UserId { get; set; }
}