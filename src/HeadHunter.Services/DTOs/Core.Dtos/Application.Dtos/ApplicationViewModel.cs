using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;
using HeadHunter.Services.DTOs.Users.Dtos;

namespace HeadHunter.Services.DTOs.Core.Dtos.Application.Dtos;

public class ApplicationViewModel
{
    public UserViewModel User { get; set; }
    public JobVacancyViewModel JobVacancy { get; set; }
}