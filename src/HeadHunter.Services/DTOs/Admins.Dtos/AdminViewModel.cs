using HeadHunter.Domain.Entities.Core;

namespace HeadHunter.Services.DTOs.Admins.Dtos;

public record AdminViewModel(
    string FirstName,
    string LastName,
    string Phone,
    string Email,
    Address Address);
