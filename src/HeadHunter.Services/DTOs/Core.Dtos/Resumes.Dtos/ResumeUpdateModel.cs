namespace HeadHunter.Services.DTOs.Core.Dtos.Resumes.Dtos;

public record ResumeUpdateModel(
    string FirstName,
    string LastName,
    string Description,
    string JobTitle,
    string Education);