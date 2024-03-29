namespace HeadHunter.Services.DTOs.Industry.Dtos;
public record IndustryCategoryViewModel(
    long Id,
    string Name,
    long ParentId
    );
