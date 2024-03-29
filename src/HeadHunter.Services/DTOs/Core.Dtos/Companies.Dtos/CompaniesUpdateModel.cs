namespace HeadHunter.Services.DTOs.Core.Dtos.Companies.Dtos;

public record CompaniesUpdateModel(
    string Name,
    long IndustryId,
    string Details,
    long AddressId);