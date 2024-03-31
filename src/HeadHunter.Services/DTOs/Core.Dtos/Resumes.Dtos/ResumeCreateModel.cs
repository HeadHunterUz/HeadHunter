namespace HeadHunter.Services.DTOs.Core.Dtos.Resumes.Dtos;

public class ResumeCreateModel
{
    public long UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
    public string JobTitle { get; set; }
    public string Education { get; set; }

}