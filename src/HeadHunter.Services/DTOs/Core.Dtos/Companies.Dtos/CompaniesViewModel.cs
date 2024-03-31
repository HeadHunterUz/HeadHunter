using HeadHunter.Services.DTOs.Core.Dtos.Address.Dtos;
using HeadHunter.Services.DTOs.Industry.Dtos.Industries.Core;

namespace HeadHunter.Services.DTOs.Core.Dtos.Companies.Dtos;

public class CompanyViewModel
{
    public string Name { get; set; }
    public IndustryViewModel Industry { get; set; }
    public string Details { get; set; }
    public AddressViewModel Address { get; set; }
}