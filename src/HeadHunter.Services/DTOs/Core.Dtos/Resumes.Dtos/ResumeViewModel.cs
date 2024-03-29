namespace HeadHunter.Services.DTOs.Core.Dtos.Resumes.Dtos;

public record ResumeViewModel(
    UserViewModel User,
    string FirstName,
    string LastName,
    string Description,
    string JobTitle,
    string Education);