namespace HeadHunter.Services.DTOs.Industry.Dtos.Industries.Core;
public record IndustryUpdateModel(
    long Id,
    string Name,
    long CategoryId
    );