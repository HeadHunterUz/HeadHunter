﻿namespace HeadHunter.Services.DTOs.Users.Dtos;

public record UserCreateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Photo { get; set; }
    public long IndustryId { get; set; }
    public long AddressId { get; set; }
}