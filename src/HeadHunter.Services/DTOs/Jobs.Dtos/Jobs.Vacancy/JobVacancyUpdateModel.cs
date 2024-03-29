using HeadHunter.Domain.Enums;

namespace HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;
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