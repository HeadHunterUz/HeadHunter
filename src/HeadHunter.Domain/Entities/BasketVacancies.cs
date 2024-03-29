using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadHunter.Domain.Entities;

public class BasketVacancies
{
    public long Id { get; set; }
    public long UserId {  get; set; }
    public User user { get; set; }
}
