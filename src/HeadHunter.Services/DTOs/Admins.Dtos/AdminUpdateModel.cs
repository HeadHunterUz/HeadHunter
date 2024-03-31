namespace HeadHunter.Services.DTOs.Admins.Dtos;

public record AdminUpdateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? AddressId { get; set; }
}
