using HeadHunter.Domain.Enums;
using HeadHunter.Services.DTOs.Core.Dtos.Address.Dtos;
using HeadHunter.Services.DTOs.Core.Dtos.Companies.Dtos;
using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Core;

namespace HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;

public class JobVacancyViewModel
{
    public long Id { get; set; }
    public JobViewModel Job { get; set; }
    public string Mission { get; set; }
    public decimal Salary { get; set; }
    public string Requirements { get; set; }
    public long CompanyId { get; set; }
    public CompanyViewModel Company { get; set; }
    public AddressViewModel Address { get; set; }
    public WorkTime WorkTime { get; set; }
    public WorkingType WorkingType { get; set; }

}