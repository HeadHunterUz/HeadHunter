using HeadHunter.Domain.Commons;
namespace HeadHunter.Domain.Entities;
public class BasketVacancies:Auditable
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public User user { get; set; }
}
