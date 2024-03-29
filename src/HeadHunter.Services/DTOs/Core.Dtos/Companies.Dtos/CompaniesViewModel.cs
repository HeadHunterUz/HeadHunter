using HeadHunter.Services.DTOs.Core.Dtos.Address.Dtos;

namespace HeadHunter.Services.DTOs.Core.Dtos.Companies.Dtos;

public record CompaniesViewModel(
    string Name,
    IndustryViewModel Industry,
    string Details,
    AddressViewModel Address);