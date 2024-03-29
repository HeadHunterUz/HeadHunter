using HeadHunter.Domain.Entities.Jobs;
using HeadHunter.Domain.Entities.Users;

namespace HeadHunter.Services.DTOs.Core.Dtos.Appication.Dtos;

public record ApplicationViewModel(
    User User,
    JobVacancy Vacancy);