using HeadHunter.Services.DTOs.Industry.Dtos.Industries.Categories;

namespace HeadHunter.Services.DTOs.Industry.Dtos.Industries.Core;

public class IndustryViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public IndustryCategoryViewModel Industry { get; set; }

}