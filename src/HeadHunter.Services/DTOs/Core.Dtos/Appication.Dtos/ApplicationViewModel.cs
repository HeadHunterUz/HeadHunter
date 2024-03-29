namespace HeadHunter.Services.DTOs.Core.Dtos.Appication.Dtos;

public record ApplicationViewModel(
    UserViewModel User,
    JobVacancyViewModel Vacancy);