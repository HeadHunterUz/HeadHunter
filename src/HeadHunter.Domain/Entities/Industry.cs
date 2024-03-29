using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadHunter.Domain.Entities;

public class Industry
{ 
    public long Id { get; set; }
    public string Name { get; set; }
    public long CategoryId {  get; set; }
    public Category category { get; set; }
}
