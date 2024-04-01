using System.Data.SqlClient;
using Dapper;
using HeadHunter.Domain.Entities.Admins;
using HeadHunter.Domain.Entities.Core;
using HeadHunter.Domain.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using Npgsql.Replication;
using HeadHunter.Services.Helpers;
using HeadHunter.DataAccess;
using HeadHunter.Domain.Enums;
using HeadHunter.Domain.Entities.Industries;
using HeadHunter.DataAccess.Repositories;

public class SeedData
{
    private readonly string _connectionString;

    public SeedData(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task SeedDatas()
    {
        var connection = new NpgsqlConnection(_connectionString);

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

        connection.Open();
        try
        {
            Repository<Company> repository = new Repository<Company>(_connectionString);

            foreach (var company in companies)
            {
                var insertedCompany = await repository.InsertAsync("Companies", company);

                if (insertedCompany != null)
                {
                    Console.WriteLine($"Company '{insertedCompany.Name}' inserted successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to insert the company.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            connection.Close();
        }
    }

    

    private IEnumerable<Admin> GetAdmins()
    {
        return new List<Admin>
        {
            new Admin
            {
                FirstName = "John",
                LastName = "Doe",
                Phone = "123456789",
                Email = "john.doe@example.com",
                Password = "password",
                AddressId = 1,
                CreatedAt = DateTime.UtcNow
            },
            // Add more admins here if needed
        };
    }
}