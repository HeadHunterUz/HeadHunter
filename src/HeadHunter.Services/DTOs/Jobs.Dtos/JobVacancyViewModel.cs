using HeadHunter.Domain.Enums;
namespace HeadHunter.Services.DTOs.Jobs.Dtos;
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
