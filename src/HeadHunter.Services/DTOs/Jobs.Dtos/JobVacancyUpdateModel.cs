using HeadHunter.Domain.Enums;
namespace HeadHunter.Services.DTOs.Jobs.Dtos;
public record JobVacancyUpdateModel(
    long Id,
    long JobId,
    string Mission,
    decimal Salary,
    string Requirements,
    long CompanyId,
    long AddressId,
    WorkTime WorkTime,
    WorkingType WorkingType);
