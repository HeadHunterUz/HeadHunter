using HeadHunter.Services.DTOs.Core.Dtos.Address.Dtos;
using HeadHunter.Services.DTOs.Industry.Dtos.Industries.Core;

namespace HeadHunter.Services.DTOs.Core.Dtos.Companies.Dtos;

public record CompaniesViewModel(
    string Name,
    IndustryViewModel Industry,
    string Details,
    AddressViewModel Address);