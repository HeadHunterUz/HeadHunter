using HeadHunter.Services.DTOs.Core.Dtos.Address.Dtos;

namespace HeadHunter.Services.DTOs.Admins.Dtos;

public record AdminViewModel(
    string FirstName,
    string LastName,
    string Phone,
    string Email,
    AddressViewModel Address);
