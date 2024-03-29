namespace HeadHunter.Services.DTOs.Core.Dtos.Companies.Dtos;

public record CompaniesCreateModel(
    string Name,
    long IndustryId,
    string Details,
    long AddressId);