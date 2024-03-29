namespace HeadHunter.Services.DTOs.Industry.Dtos.Industries.Categories;
public record IndustryCategoryUpdateModel(
    long Id,
    string Name,
    long ParentId
    );