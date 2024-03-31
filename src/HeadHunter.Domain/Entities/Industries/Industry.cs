﻿using HeadHunter.Domain.Commons;

namespace HeadHunter.Domain.Entities.Industries;

public class Industry : Auditable
{
    public string Name { get; set; }
    public long CategoryId { get; set; }
    public IndustryCategories IndustryCategories { get; set; }
}