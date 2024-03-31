﻿using HeadHunter.Domain.Commons;
using HeadHunter.Domain.Entities.Core;

namespace HeadHunter.Domain.Entities.Admins;

public class Admin : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public long AddressId { get; set; }
}