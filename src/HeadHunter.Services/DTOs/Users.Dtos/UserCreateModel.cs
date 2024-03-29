namespace HeadHunter.Services.DTOs.Users.Dtos;

public record UserCreateModel(
    string FirstName,
    string LastName,
    string Phone,
    string Email,
    string Password,
    string Photo,
    long IndustryId,
    long AddressId);
