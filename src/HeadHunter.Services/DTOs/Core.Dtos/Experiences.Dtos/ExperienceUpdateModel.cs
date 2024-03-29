namespace HeadHunter.Services.DTOs.Core.Dtos.Experiences.Dtos;

public record ExperienceUpdateModel(
    long CompanyId,
    string JobTitle,
    string Position,
    DateTime StartTime,
    DateTime? EndTime);
