namespace HeadHunter.Services.DTOs.Industry.Dtos.Industries.Categories;
public record IndustryCategoryViewModel(
    long Id,
    string Name,
    long ParentId
    );