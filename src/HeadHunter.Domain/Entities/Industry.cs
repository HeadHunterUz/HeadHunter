using HeadHunter.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadHunter.Domain.Entities;

public class Industry:Auditable
{ 
    public long Id { get; set; }
    public string Name { get; set; }
    public long CategoryId {  get; set; }
    public IndustryCategories industryCategories { get; set; }
}
