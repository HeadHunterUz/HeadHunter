namespace HeadHunter.Services.DTOs.Admins.Dtos;

public record AdminUpdateModel(
        string FirstName,
        string LastName,
        string Phone,
        string Email,
        string Password,
        string? AddressId);
