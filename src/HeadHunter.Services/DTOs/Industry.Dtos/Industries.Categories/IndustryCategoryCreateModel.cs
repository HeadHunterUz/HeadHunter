namespace HeadHunter.Services.DTOs.Industry.Dtos.Industries.Categories;
public record IndustryCategoryCreateModel(
    string Name,
    long ParentId
    );