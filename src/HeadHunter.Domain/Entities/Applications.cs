using HeadHunter.Domain.Enums;
namespace HeadHunter.Domain.Entities;
public class Applications
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public User user { get; set; }
    public string ApplyStatus { get; set; }//enum
    public ApplyStatus applyStatus { get; set; }
    public long VacancyId { get; set; }
    public JobVacancies jobVacancies { get; set; }
    public long CompanyId { get; set; }
    public Company company { get; set; }
}
