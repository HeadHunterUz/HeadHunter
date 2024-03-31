using HeadHunter.Services.DTOs.Core.Dtos.Address.Dtos;

namespace HeadHunter.Services.DTOs.Admins.Dtos;

public record AdminViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public AddressViewModel Address { get; set; }
}
