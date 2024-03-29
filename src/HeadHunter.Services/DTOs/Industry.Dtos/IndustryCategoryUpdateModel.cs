namespace HeadHunter.Services.DTOs.Industry.Dtos;
public record IndustryCategoryUpdateModel(
    long Id,
    string Name,
    long ParentId
    );
