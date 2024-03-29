namespace HeadHunter.Services.DTOs.Industry.Dtos;
public record IndustryCategoryCreateModel(
    string Name,
    long ParentId
    );
