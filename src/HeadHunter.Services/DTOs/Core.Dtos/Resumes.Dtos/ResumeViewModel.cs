using HeadHunter.Services.DTOs.Users.Dtos;

namespace HeadHunter.Services.DTOs.Core.Dtos.Resumes.Dtos;

public class ResumeViewModel
{
    public long Id { get; set; }
    public UserViewModel User { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
    public string JobTitle { get; set; }
    public string Education { get; set; }
}