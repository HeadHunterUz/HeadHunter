using AutoMapper;
using HeadHunter.DataAccess;
using HeadHunter.DataAccess.IRepositories;
using HeadHunter.DataAccess.Repositories;
using HeadHunter.Domain.Entities.Core;
using HeadHunter.Services.Mappers;
using HeadHunter.Services.Services.Addresses;
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

builder.Services.AddScoped<IRepository<Address>, Repository<Address>>( _ => new Repository<Address>(Constants.DbConnectionString));
builder.Services.AddScoped<IAddressService, AddressService>();


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