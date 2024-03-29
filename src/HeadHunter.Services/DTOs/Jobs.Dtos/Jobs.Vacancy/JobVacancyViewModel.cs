using HeadHunter.Domain.Enums;
using HeadHunter.Services.DTOs.Core.Dtos.Address.Dtos;

namespace HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;

public record JobVacancyViewModel(
     long Id,
    JobVacancyViewModel Job,
    string Mission,
    decimal Salary,
    string Requirements,
    long CompanyId,
    AddressViewModel Address,
    WorkTime WorkTime,
    WorkingType WorkingType);