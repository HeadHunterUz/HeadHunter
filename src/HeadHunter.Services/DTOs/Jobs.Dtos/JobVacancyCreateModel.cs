using HeadHunter.Domain.Enums;
namespace HeadHunter.Services.DTOs.Jobs.Dtos;
public record JobVacancyCreateModel(
    long JobId,
    string Mission,
    decimal Salary,
    string Requirements,
    long CompanyId,
    long AddressId,
    WorkTime WorkTime,
    WorkingType WorkingType);
