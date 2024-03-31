using HeadHunter.Domain.Commons;
using HeadHunter.Domain.Entities.Core;
using HeadHunter.Domain.Enums;

namespace HeadHunter.Domain.Entities.Jobs;

public class JobVacancy : Auditable
{
    /// <summary>
    /// JobId for vacancy
    /// </summary>
    public long JobId { get; set; }
    /// <summary>
    /// Mission
    /// </summary>
    public string Mission { get; set; }
    /// <summary>
    /// Salary
    /// </summary>
    public decimal Salary { get; set; }
    /// <summary>
    /// Job's requirements
    /// </summary>
    public string Requirements { get; set; }
    /// <summary>
    /// CompanyId
    /// </summary>
    public long CompanyId { get; set; }
    /// <summary>
    /// AddressId
    /// </summary>
    public long AddressId { get; set; }
    /// <summary>
    /// WorkTime
    /// </summary>
    public WorkTime WorkTime { get; set; }
    /// <summary>
    /// WorkingType
    /// </summary>
    public WorkingType WorkingType { get; set; }
}