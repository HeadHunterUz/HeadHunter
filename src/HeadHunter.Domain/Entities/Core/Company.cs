using HeadHunter.Domain.Commons;
using HeadHunter.Domain.Entities.Industries;

namespace HeadHunter.Domain.Entities.Core;

public class Company : Auditable
{
    /// <summary>
    /// Company's Name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Industry Id 
    /// </summary>
    public long IndustryId { get; set; }
    /// <summary>
    /// Company's Details
    /// </summary>
    public string Details { get; set; }
    /// <summary>
    /// Company's AddressId
    /// </summary>
    public long AddressId { get; set; }
}