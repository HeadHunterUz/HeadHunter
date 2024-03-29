using HeadHunter.Domain.Commons;
using HeadHunter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HeadHunter.Domain.Entities;
public class JobVacancies : Auditable
{
    public long JobId { get; set; }
    public Jobs jobs { get; set; }
    public string Mission {  get; set; }
    public decimal Salary {  get; set; }
    public string Requirements {  get; set; }
    public long CompanyId {  get; set; }
    public Company company { get; set; }
    public long AddressId {  get; set; }
    public Address address { get; set; }
    public WorkTime workTime { get; set; }
    public WorkingType workingType { get; set; }

}
