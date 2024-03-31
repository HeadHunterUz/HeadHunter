using HeadHunter.Domain.Enums;

namespace HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;
public record JobVacancyCreateModel
{

    public long JobId { get; set; }
    public string Mission { get; set; }
    public decimal Salary { get; set; }
    public string Requirements { get; set; }
    public long CompanyId { get; set; }
    public long AddressId { get; set; }
    public WorkTime WorkTime { get; set; }
    public WorkingType WorkingType { get; set; }
}