using AutoMapper;
using HeadHunter.Domain.Entities.Admins;
using HeadHunter.Domain.Entities.Core;
using HeadHunter.Domain.Entities.Industries;
using HeadHunter.Domain.Entities.Jobs;
using HeadHunter.Domain.Entities.Users;
using HeadHunter.Domain.Entities.Vacancies;
using HeadHunter.Services.DTOs.Admins.Dtos;
using HeadHunter.Services.DTOs.Core.Dtos.Address.Dtos;
using HeadHunter.Services.DTOs.Core.Dtos.Application.Dtos;
using HeadHunter.Services.DTOs.Core.Dtos.Companies.Dtos;
using HeadHunter.Services.DTOs.Core.Dtos.Experiences.Dtos;
using HeadHunter.Services.DTOs.Core.Dtos.Resumes.Dtos;
using HeadHunter.Services.DTOs.Industry.Dtos.Industries.Categories;
using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Core;
using HeadHunter.Services.DTOs.Jobs.Dtos.Jobs.Vacancy;
using HeadHunter.Services.DTOs.Users.Dtos;
using HeadHunter.Services.DTOs.Vacancies.Dtos.BasketVacancies;
using HeadHunter.Services.DTOs.Vacancies.Dtos.VacancySkills;

namespace HeadHunter.Services.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserViewModel>().ReverseMap();
        CreateMap<User, UserUpdateModel>().ReverseMap();
        CreateMap<User, UserCreateModel>().ReverseMap();

        CreateMap<Admin, AdminViewModel>().ReverseMap();
        CreateMap<Admin, AdminUpdateModel>().ReverseMap();
        CreateMap<Admin, AdminCreateModel>().ReverseMap();

        CreateMap<Address, AddressViewModel>().ReverseMap();
        CreateMap<Address, AddressUpdateModel>().ReverseMap();
        CreateMap<Address, AddressCreateModel>().ReverseMap();

        CreateMap<Application, ApplicationViewModel>().ReverseMap();
        CreateMap<Application, ApplicationUpdateModel>().ReverseMap();
        CreateMap<Application, ApplicationCreateModel>().ReverseMap();

        CreateMap<Company, CompanyViewModel>().ReverseMap();
        CreateMap<Company, CompanyUpdateModel>().ReverseMap();
        CreateMap<Company, CompanyViewModel>().ReverseMap();

        CreateMap<Experience, ExperienceViewModel>().ReverseMap();
        CreateMap<Experience, ExperienceUpdateModel>().ReverseMap();
        CreateMap<Experience, ExperienceCreateModel>().ReverseMap();

        CreateMap<Resume, ResumeViewModel>().ReverseMap();
        CreateMap<Resume, ResumeUpdateModel>().ReverseMap();
        CreateMap<Resume, ResumeCreateModel>().ReverseMap();

        CreateMap<Industry, IndustryCategoryViewModel>().ReverseMap();
        CreateMap<Industry, IndustryCategoryUpdateModel>().ReverseMap();
        CreateMap<Industry, IndustryCategoryCreateModel>().ReverseMap();

        CreateMap<IndustryCategory, IndustryCategoryViewModel>().ReverseMap();
        CreateMap<IndustryCategory, IndustryCategoryUpdateModel>().ReverseMap();
        CreateMap<IndustryCategory, IndustryCategoryCreateModel>().ReverseMap();

        CreateMap<Job, JobViewModel>().ReverseMap();
        CreateMap<Job, JobUpdateModel>().ReverseMap();
        CreateMap<Job, JobCreateModel>().ReverseMap();

        CreateMap<JobVacancy, JobVacancyViewModel>().ReverseMap();
        CreateMap<JobVacancy, JobVacancyUpdateModel>().ReverseMap();
        CreateMap<JobVacancy, JobVacancyCreateModel>().ReverseMap();

        CreateMap<BasketVacancies, BasketVacancyViewModel>().ReverseMap();
        CreateMap<BasketVacancies, BasketVacancyUpdateModel>().ReverseMap();
        CreateMap<BasketVacancies, BasketVacancyCreateModel>().ReverseMap();

        CreateMap<VacancySkill, VacancySkillViewModel>().ReverseMap();
        CreateMap<VacancySkill, VacancySkillUpdateModel>().ReverseMap();
        CreateMap<VacancySkill, VacancySkillCreateModel>().ReverseMap();
    }
}
