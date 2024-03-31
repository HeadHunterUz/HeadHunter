using System.Data;
using System.Security.Cryptography;
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
using HeadHunter.Services.Helpers;
using HeadHunter.Services.Mappers;
using HeadHunter.Services.Services.Addresses;
using HeadHunter.Services.Services.IndustryCategories;
using Microsoft.Extensions.Configuration;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new NpgsqlConnection(connectionString);
});

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IMapper, Mapper>();

builder.Services.AddScoped<IRepository<Admin>, Repository<Admin>>( _ => new Repository<Admin>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<Address>, Repository<Address>>( _ => new Repository<Address>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<Application>, Repository<Application>>( _ => new Repository<Application>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<Company>, Repository<Company>>( _ => new Repository<Company>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<Experience>, Repository<Experience>>( _ => new Repository<Experience>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<Industry>, Repository<Industry>>( _ => new Repository<Industry>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<IndustryCategory>, Repository<IndustryCategory>>( _ => new Repository<IndustryCategory>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<Job>, Repository<Job>>( _ => new Repository<Job>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<JobVacancy>, Repository<JobVacancy>>( _ => new Repository<JobVacancy>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<User>, Repository<User>>( _ => new Repository<User>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<UserSkills>, Repository<UserSkills>>( _ => new Repository<UserSkills>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<BasketVacancies>, Repository<BasketVacancies>>( _ => new Repository<BasketVacancies>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<VacancySkill>, Repository<VacancySkill>>( _ => new Repository<VacancySkill>(Constants.DbConnectionString));
builder.Services.AddScoped<IRepository<UserSkills>, Repository<UserSkills>>( _ => new Repository<UserSkills>(Constants.DbConnectionString));


// Add services
builder.Services.AddScoped<IIndustryCategoryService, IndustryCategoryService>();
builder.Services.AddScoped<IAddressService, AddressService>();

IDbConnection dbConnection = new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));

var tableCreator = new TableCreator(dbConnection);

tableCreator.CreateTable<Address>(Constants.AddressTableName);

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