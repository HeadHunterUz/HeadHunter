using HeadHunter.Domain.Commons;
using HeadHunter.Domain.Entities.Core;
using HeadHunter.Domain.Enums;

namespace HeadHunter.Domain.Entities.Jobs;

public class JobVacancy : Auditable
{
    public long JobId { get; set; }
    public Job Jobs { get; set; }
    public string Mission { get; set; }
    public decimal Salary { get; set; }
    public string Requirements { get; set; }
    public long CompanyId { get; set; }
    public Company Company { get; set; }
    public long AddressId { get; set; }
    public Address Address { get; set; }
    public WorkTime WorkTime { get; set; }
    public WorkingType WorkingType { get; set; }
}