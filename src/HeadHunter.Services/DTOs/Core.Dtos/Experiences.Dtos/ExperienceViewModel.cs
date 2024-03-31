using HeadHunter.Services.DTOs.Core.Dtos.Companies.Dtos;
using HeadHunter.Services.DTOs.Users.Dtos;

namespace HeadHunter.Services.DTOs.Core.Dtos.Experiences.Dtos;

public class ExperienceViewModel
{
    public UserViewModel User { get; set; }
    public CompanyViewModel Company { get; set; }
    public string JobTitle { get; set; }
    public string Position { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }

}