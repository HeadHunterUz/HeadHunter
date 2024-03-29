using HeadHunter.Services.DTOs.Core.Dtos.Address.Dtos;

namespace HeadHunter.Services.DTOs.Users.Dtos;

public record UserViewModel(
    string FirstName,
    string LastName,
    string Phone,
    string Email,
    string Password,
    string Photo,
    IndustryViewModel Industry,
    AddressViewModel Address);