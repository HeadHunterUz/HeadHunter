using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;
using HeadHunter.Services.DTOs.Users.Dtos;

namespace HeadHunter.Services.DTOs.Vacancies.Dtos.BasketVacancies;

public record BasketVacancyViewModel
{
    public long Id { get; set; }
    public JobVacancyViewModel JobVacancy { get; set; }
    public UserViewModel User { get; set; }
}