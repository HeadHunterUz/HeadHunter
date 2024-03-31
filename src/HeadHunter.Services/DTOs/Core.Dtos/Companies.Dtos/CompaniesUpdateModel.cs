namespace HeadHunter.Services.DTOs.Core.Dtos.Companies.Dtos;

public class CompaniesUpdateModel
{
    public string Name { get; set; }
    public long IndustryId { get; set; }
    public string Details { get; set; }
    public long AddressId { get; set; }

}