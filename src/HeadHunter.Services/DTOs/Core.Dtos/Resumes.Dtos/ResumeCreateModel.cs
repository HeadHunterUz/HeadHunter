namespace HeadHunter.Services.DTOs.Core.Dtos.Resumes.Dtos;

public record ResumeCreateModel(
    long UserId,
    string FirstName,
    string LastName,
    string Description,
    string JobTitle,
    string Education);