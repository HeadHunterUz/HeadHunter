using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities.Core;

public class Address : Auditable
{
    /// <summary>
    /// Country Name
    /// </summary>
    public string Country { get; set; }
    /// <summary>
    /// City Name
    /// </summary>
    public string City { get; set; }
}