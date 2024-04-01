using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;
using HeadHunter.Services.DTOs.Users.Dtos;

namespace HeadHunter.Services.DTOs.Core.Dtos.Application.Dtos;

public class ApplicationViewModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long JobVacancyId { get; set; }
}