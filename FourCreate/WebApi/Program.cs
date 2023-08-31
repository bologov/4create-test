using System.Reflection;
using System.Text.Json.Serialization;
using Application.Contracts.Company;
using Application.Contracts.Employee;
using Application.Services;
using Common;
using Data;
using Data.Repositories;
using Domain.Managers;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers()
            .AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            var apiAssembly = Assembly.GetExecutingAssembly();
            options.IncludeXmlComments(GetXmlDocumentationFileFor(apiAssembly));

            var contractsAssembly = typeof(Application.Contracts.Company.CreateCompanyDto).Assembly;
            options.IncludeXmlComments(GetXmlDocumentationFileFor(contractsAssembly));

            var sharedAssembly = typeof(Domain.Shared.EmployeeTitle).Assembly;
            options.IncludeXmlComments(GetXmlDocumentationFileFor(sharedAssembly));

            static string GetXmlDocumentationFileFor(Assembly assembly)
            {
                var documentationFile = $"{assembly.GetName().Name}.xml";
                var path = Path.Combine(AppContext.BaseDirectory, documentationFile);

                return path;
            }
        });

        InitialiseServices(builder.Services, builder.Environment, builder.Configuration);

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
    }

    private static void InitialiseServices(IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
    {
        // Common
        services.AddScoped<IGuidGenerator, GuidGenerator>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        // Application
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ICompanyService, CompanyService>();

        // Domain
        services.AddScoped<ICompanyManager, CompanyManager>();
        services.AddScoped<IEmployeeManager, EmployeeManager>();

        // Data
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

        var connectionString = configuration.GetConnectionString(nameof(ApplicationDbContext));
        var serverVersion = ServerVersion.AutoDetect(connectionString);

        services.AddDbContext<ApplicationDbContext>(
            options =>
            {
                options.UseMySql(connectionString, serverVersion);

                if (environment.IsDevelopment())
                {
                    options.LogTo(Console.WriteLine, LogLevel.Information)
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors();
                }
            });
    }
}

