using HeadHunter.Services.DTOs.Industry.Dtos.Industries.Categories;

namespace HeadHunter.Services.DTOs.Industry.Dtos.Industries.Core;

public record IndustryViewModel(
    long Id,
    string Name,
    IndustryCategoryViewModel Industry);