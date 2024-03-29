namespace HeadHunter.Services.DTOs.Core.Dtos.Experiences.Dtos;

public record ExperienceCreateModel(
    long UserId,
    long CompanyId,
    string JobTitle,
    string Position,
    DateTime StartTime,
    DateTime? EndTime);
