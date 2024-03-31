namespace HeadHunter.Services.DTOs.Core.Dtos.Experiences.Dtos;

public class ExperienceUpdateModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long CompanyId { get; set; }
    public string JobTitle { get; set; }
    public string Position { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }

}