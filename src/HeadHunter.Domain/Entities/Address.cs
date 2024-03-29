using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadHunter.Domain.Entities;

public class Address
{
    public long Id { get; set; }
    public string Country {  get; set; }
    public string City { get; set; }
}
