using HeadHunter.Domain.Commons;
using HeadHunter.Domain.Entities.Users;
namespace HeadHunter.Domain.Entities;
public class BasketVacancies:Auditable
{
    public long UserId { get; set; }
    public User user { get; set; }
}
