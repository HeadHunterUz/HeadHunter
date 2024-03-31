using HeadHunter.Domain.Commons;
using HeadHunter.Domain.Entities.Core;

namespace HeadHunter.Domain.Entities.Admins;

public class Admin : Auditable
{
    /// <summary>
    /// admin's FirstName
    /// </summary>
    public string FirstName { get; set; }
    /// <summary>
    /// admin's LastName
    /// </summary>
    public string LastName { get; set; }
    /// <summary>
    /// admin's PhoneNumber
    /// </summary>
    public string Phone { get; set; }
    /// <summary>
    /// admin's Email
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// admin's Password
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// admin's Address
    /// </summary>
    public long AddressId { get; set; }
}