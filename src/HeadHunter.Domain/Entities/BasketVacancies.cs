namespace HeadHunter.Domain.Entities;

public class BasketVacancies
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public User user { get; set; }
}
