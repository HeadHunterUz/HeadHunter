using System.Data;
using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.DataAccess.Repositories;
using HeadHunter.Domain.Entities.Admins;
using HeadHunter.Domain.Entities.Core;
using HeadHunter.Domain.Entities.Industries;
using HeadHunter.Domain.Entities.Jobs;
using HeadHunter.Domain.Entities.Users;
using HeadHunter.Domain.Entities.Vacancies;
using HeadHunter.Domain.Enums;
using HeadHunter.Services.DTOs.Core.Dtos.Address.Dtos;
using HeadHunter.Services.Helpers;
using HeadHunter.Services.Mappers;
using HeadHunter.Services.Services.Addresses;
using HeadHunter.Services.Services.Admins;
using HeadHunter.Services.Services.Applications;
using HeadHunter.Services.Services.BasketVacancies;
using HeadHunter.Services.Services.Companies;
using HeadHunter.Services.Services.Experiences;
using HeadHunter.Services.Services.Industries;
using HeadHunter.Services.Services.IndustryCategories;
using HeadHunter.Services.Services.Jobs;
using HeadHunter.Services.Services.JobVacancies;
using HeadHunter.Services.Services.Resumes;
using HeadHunter.Services.Services.Users;
using HeadHunter.Services.Services.VacancySkills;
using Microsoft.Extensions.Configuration;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
MapperConfiguration configuration = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Address, AddressViewModel>().ReverseMap();
    // Add other mapping configurations here if needed
});
builder.Services.AddSingleton(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("LocalConnection");
    return new NpgsqlConnection(connectionString);
});
// Assuming you have an instance of MapperConfiguration called "configuration"
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IMapper, Mapper>();

builder.Services.AddScoped<IRepository<Admin>, Repository<Admin>>( _ => new Repository<Admin>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<Address>, Repository<Address>>( _ => new Repository<Address>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<Application>, Repository<Application>>( _ => new Repository<Application>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<User>, Repository<User>>( _ => new Repository<User>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<IndustryCategory>, Repository<IndustryCategory>>( _ => new Repository<IndustryCategory>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<Industry>, Repository<Industry>>( _ => new Repository<Industry>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<Company>, Repository<Company>>( _ => new Repository<Company>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<Address>, Repository<Address>>( _ => new Repository<Address>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<Job>, Repository<Job>>( _ => new Repository<Job>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<JobVacancy>, Repository<JobVacancy>>( _ => new Repository<JobVacancy>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<BasketVacancy>, Repository<BasketVacancy>>( _ => new Repository<BasketVacancy>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<VacancySkill>, Repository<VacancySkill>>( _ => new Repository<VacancySkill>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<Experience>, Repository<Experience>>( _ => new Repository<Experience>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<Resume>, Repository<Resume>>( _ => new Repository<Resume>(Constants.DbConnectionString));


// Add services
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IIndustryCategoryService, IndustryCategoryService>();
builder.Services.AddScoped<IIndustryService, IndustryService>();
builder.Services.AddScoped<IResumeService, ResumeService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IJobVacancyService, JobVacancyService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IExperienceService, ExperienceService>();
builder.Services.AddScoped<IBasketVacancyService, BasketVacancyService>();
builder.Services.AddScoped<IVacancySkillService, VacancySkillService>();

IDbConnection dbConnection = new NpgsqlConnection(builder.Configuration.GetConnectionString("LocalConnection"));

var tableCreator = new TableCreator(dbConnection);

tableCreator.CreateTable<Admin>(Constants.AdminTableName);
tableCreator.CreateTable<Address>(Constants.AddressTableName);
tableCreator.CreateTable<Application>(Constants.ApplicationTableName);
tableCreator.CreateTable<Company>(Constants.CompanyTableName);
tableCreator.CreateTable<Address>(Constants.ExperienceTableName);
tableCreator.CreateTable<Industry>(Constants.IndustryTableName);
tableCreator.CreateTable<IndustryCategory>(Constants.IndustryCategoryTableName);
tableCreator.CreateTable<Job>(Constants.JobTableName);
tableCreator.CreateTable<JobVacancy>(Constants.JobVacancyTableName);
tableCreator.CreateTable<User>(Constants.UserTableName);
tableCreator.CreateTable<BasketVacancy>(Constants.BasketVacancyTableName);
tableCreator.CreateTable<VacancySkill>(Constants.VacancySkillTableName);


var connection = new NpgsqlConnection(Constants.DbConnectionString);

try
{
    connection.Open();

    var addresses = new List<Address>();
    addresses.Add(new Address() { Country = "USA", City = "New York" });
    addresses.Add(new Address() { Country = "Canada", City = "Toronto" });
    addresses.Add(new Address() { Country = "UK", City = "London" });
    addresses.Add(new Address() { Country = "France", City = "Paris" });
    addresses.Add(new Address() { Country = "Germany", City = "Berlin" });
    addresses.Add(new Address() { Country = "Australia", City = "Sydney" });
    addresses.Add(new Address() { Country = "Japan", City = "Tokyo" });
    addresses.Add(new Address() { Country = "Brazil", City = "Rio de Janeiro" });
    addresses.Add(new Address() { Country = "India", City = "Mumbai" });
    addresses.Add(new Address() { Country = "China", City = "Beijing" });

    var adminsRepository = new Repository<Admin>(Constants.LocalDbConnectionString);
    var admins = new List<Admin>
    {
        new Admin { FirstName = "John", LastName = "Doe", Phone = "1234567890", Email = "john.doe@example.com", Password = "password1", AddressId = 1 },
        new Admin { FirstName = "Jane", LastName = "Smith", Phone = "0987654321", Email = "jane.smith@example.com", Password = "password2", AddressId = 2 },
        new Admin { FirstName = "Alice", LastName = "Johnson", Phone = "5556667777", Email = "alice.johnson@example.com", Password = "password3", AddressId = 3 },
        new Admin { FirstName = "Bob", LastName = "Brown", Phone = "9876543210", Email = "bob.brown@example.com", Password = "password4", AddressId = 4 },
        new Admin { FirstName = "Eve", LastName = "White", Phone = "1112223333", Email = "eve.white@example.com", Password = "password5", AddressId = 5 },
        new Admin { FirstName = "Michael", LastName = "Davis", Phone = "4445556666", Email = "michael.davis@example.com", Password = "password6", AddressId = 6 },
        new Admin { FirstName = "Sarah", LastName = "Lee", Phone = "2223334444", Email = "sarah.lee@example.com", Password = "password7", AddressId = 7 },
        new Admin { FirstName = "David", LastName = "Martinez", Phone = "7778889999", Email = "david.martinez@example.com", Password = "password8", AddressId = 8 },
        new Admin { FirstName = "Emily", LastName = "Garcia", Phone = "6667778888", Email = "emily.garcia@example.com", Password = "password9", AddressId = 9 },
        new Admin { FirstName = "Chris", LastName = "Lopez", Phone = "3334445555", Email = "chris.lopez@example.com", Password = "password10", AddressId = 10 }
    };
    int i = 1;
    // foreach (var admin in admins)
    // {
    //     admin.Id = i++;
    //     var insertedAdmin = await adminsRepository.InsertAsync(Constants.AdminTableName, admin);
    // }

    var applicationsRepository = new Repository<Application>(Constants.LocalDbConnectionString);

    var applications = new List<Application>();

    applications.Add(new Application() { UserId = 1, ApplyStatus = ApplyStatus.Pending, VacancyId = 101, CompanyId = 201 });
    applications.Add(new Application() { UserId = 2, ApplyStatus = ApplyStatus.Pending, VacancyId = 102, CompanyId = 202 });
    applications.Add(new Application() { UserId = 3, ApplyStatus = ApplyStatus.Pending, VacancyId = 103, CompanyId = 203 });
    applications.Add(new Application() { UserId = 4, ApplyStatus = ApplyStatus.Pending, VacancyId = 104, CompanyId = 204 });
    applications.Add(new Application() { UserId = 5, ApplyStatus = ApplyStatus.Pending, VacancyId = 105, CompanyId = 205 });
    applications.Add(new Application() { UserId = 6, ApplyStatus = ApplyStatus.Pending, VacancyId = 106, CompanyId = 206 });
    applications.Add(new Application() { UserId = 7, ApplyStatus = ApplyStatus.Pending, VacancyId = 107, CompanyId = 207 });
    applications.Add(new Application() { UserId = 8, ApplyStatus = ApplyStatus.Pending, VacancyId = 108, CompanyId = 208 });
    applications.Add(new Application() { UserId = 9, ApplyStatus = ApplyStatus.Pending, VacancyId = 109, CompanyId = 209 });
    applications.Add(new Application() { UserId = 10, ApplyStatus = ApplyStatus.Pending, VacancyId = 110, CompanyId = 210 });
    int b = 1;
    // foreach (var application in applications)
    // {
    //     application.Id = b++;
    //     var insertedApplication = await applicationsRepository.InsertAsync(Constants.ApplicationTableName, application);
    // }

    var companyRepository = new Repository<Company>(Constants.LocalDbConnectionString);
    var companies = new List<Company>();

    companies.Add(new Company() { Name = "ABC Company", IndustryId = 1, Details = "ABC Company details", AddressId = 1 });
    companies.Add(new Company() { Name = "XYZ Corporation", IndustryId = 2, Details = "XYZ Corporation details", AddressId = 2 });
    companies.Add(new Company() { Name = "123 Enterprises", IndustryId = 3, Details = "123 Enterprises details", AddressId = 3 });
    companies.Add(new Company() { Name = "BestCo Ltd", IndustryId = 4, Details = "BestCo Ltd details", AddressId = 4 });
    companies.Add(new Company() { Name = "Tech Solutions Inc", IndustryId = 5, Details = "Tech Solutions Inc details", AddressId = 5 });
    companies.Add(new Company() { Name = "Global Innovations", IndustryId = 6, Details = "Global Innovations details", AddressId = 6 });
    companies.Add(new Company() { Name = "Sunrise Enterprises", IndustryId = 7, Details = "Sunrise Enterprises details", AddressId = 7 });
    companies.Add(new Company() { Name = "Moonlight Co", IndustryId = 8, Details = "Moonlight Co details", AddressId = 8 });
    companies.Add(new Company() { Name = "Star Corp", IndustryId = 9, Details = "Star Corp details", AddressId = 9 });
    companies.Add(new Company() { Name = "Skyline Solutions", IndustryId = 10, Details = "Skyline Solutions details", AddressId = 10 });

    // foreach (var company in companies)
    // {
    //     company.Id = 2;
    //     var insertedCompany = await companyRepository.InsertAsync(Constants.CompanyTableName, company);
    // }

    var industriesRepository = new Repository<Industry>(Constants.LocalDbConnectionString);
    var industries = new List<Industry>();
    
    industries.Add(new Industry() { Name = "Automotive", CategoryId = 1 });
    industries.Add(new Industry() { Name = "Technology", CategoryId = 2 });
    industries.Add(new Industry() { Name = "Healthcare", CategoryId = 3 });
    industries.Add(new Industry() { Name = "Finance", CategoryId = 4 });
    industries.Add(new Industry() { Name = "Education", CategoryId = 5 });
    industries.Add(new Industry() { Name = "Retail", CategoryId = 6 });
    industries.Add(new Industry() { Name = "Hospitality", CategoryId = 7 });
    industries.Add(new Industry() { Name = "Manufacturing", CategoryId = 8 });
    industries.Add(new Industry() { Name = "Telecommunications", CategoryId = 9 });
    industries.Add(new Industry() { Name = "Energy", CategoryId = 10 });

    // foreach (var industry in industries)
    // {
    //     industry.Id = 1;
    //     var insertedCompany = await industriesRepository.InsertAsync(Constants.IndustryTableName, industry);
    // }

    var experiencesRepository = new Repository<Experience>(Constants.LocalDbConnectionString);
    var experiences = new List<Experience>();

    experiences.Add(new Experience() { UserId = 1, CompanyId = 1, JobTitle = "Software Engineer", Position = "Senior Developer", StartTime = DateTime.Now.AddYears(-2), EndTime = null });
    experiences.Add(new Experience() { UserId = 1, CompanyId = 2, JobTitle = "Data Analyst", Position = "Data Scientist", StartTime = DateTime.Now.AddYears(-3), EndTime = DateTime.Now.AddYears(-1) });
    experiences.Add(new Experience() { UserId = 2, CompanyId = 3, JobTitle = "Project Manager", Position = "Team Lead", StartTime = DateTime.Now.AddYears(-1), EndTime = null });
    experiences.Add(new Experience() { UserId = 2, CompanyId = 4, JobTitle = "QA Engineer", Position = "Tester", StartTime = DateTime.Now.AddYears(-2), EndTime = DateTime.Now.AddYears(-1) });
    experiences.Add(new Experience() { UserId = 3, CompanyId = 5, JobTitle = "UI/UX Designer", Position = "Lead Designer", StartTime = DateTime.Now.AddYears(-3), EndTime = DateTime.Now.AddYears(-2) });
    experiences.Add(new Experience() { UserId = 3, CompanyId = 6, JobTitle = "Marketing Specialist", Position = "Digital Marketer", StartTime = DateTime.Now.AddYears(-1), EndTime = null });
    experiences.Add(new Experience() { UserId = 4, CompanyId = 7, JobTitle = "Financial Analyst", Position = "Senior Analyst", StartTime = DateTime.Now.AddYears(-2), EndTime = DateTime.Now.AddYears(-1) });
    experiences.Add(new Experience() { UserId = 4, CompanyId = 8, JobTitle = "HR Manager", Position = "Recruitment Lead", StartTime = DateTime.Now.AddYears(-3), EndTime = DateTime.Now.AddYears(-2) });
    experiences.Add(new Experience() { UserId = 5, CompanyId = 9, JobTitle = "Sales Executive", Position = "Account Manager", StartTime = DateTime.Now.AddYears(-1), EndTime = null });
    experiences.Add(new Experience() { UserId = 5, CompanyId = 10, JobTitle = "Operations Manager", Position = "Operations Lead", StartTime = DateTime.Now.AddYears(-2), EndTime = DateTime.Now.AddYears(-1) });

    // foreach (var experience in experiences)
    // {
    //     experience.Id = 1;
    //     var insertedExperience = await experiencesRepository.InsertAsync(Constants.ExperienceTableName, experience);        
    // }

    connection.Close();
}
catch (Exception ex)
{
    Console.WriteLine("An error occurred: " + ex.Message);
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
