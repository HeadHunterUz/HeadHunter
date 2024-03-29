using HeadHunter.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadHunter.Domain.Entities;
public class Company:Auditable
{
    public string Name { get; set; }
    public long IndustryId { get; set; }
    public Industry industry { get; set; }
    public string Details {  get; set; }
    public long AddressId {  get; set; }
    public Address address { get; set; }
   }
