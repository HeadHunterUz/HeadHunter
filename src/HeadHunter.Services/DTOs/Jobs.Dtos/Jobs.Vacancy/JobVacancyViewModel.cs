using HeadHunter.Domain.Enums;
using HeadHunter.Services.DTOs.Core.Dtos.Address.Dtos;
using HeadHunter.Services.DTOs.Core.Dtos.Companies.Dtos;
using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Core;

namespace HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;

public class JobVacancyViewModel
{
    public long Id { get; set; }
    public long JobId { get; set; }
    public string Mission { get; set; }
    public decimal Salary { get; set; }
    public string Requirements { get; set; }
    public long CompanyId { get; set; }
    public long AddressId { get; set; }
    public WorkTime WorkTime { get; set; }
    public WorkingType WorkingType { get; set; }
}