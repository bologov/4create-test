using System.Reflection;
using System.Text.Json.Serialization;
using Application.Contracts.Company;
using Application.Contracts.Employee;
using Application.Services;
using Application.Validators;
using Common;
using Data;
using Data.Repositories;
using Data.UnitOfWork;
using Domain.Exceptions;
using Domain.Managers;
using Domain.Repository;
using Domain.UnitOfWork;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

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

        ConfigureServices(builder.Services, builder.Environment, builder.Configuration);

        var app = builder.Build();

        app.UseProblemDetails();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            // create db if not exists only in development mode!
            SetupDb();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();

        void SetupDb()
        {
            using var serviceScope = (app as IApplicationBuilder).ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

            var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();
        }
    }

    private static void ConfigureServices(IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
    {
        // Common
        services.AddProblemDetails(options =>
        {
            // by using mapping, business exception is converted into 409 Conflict response
            options.Map<BusinessException>((ctx, ex) =>
            {
                var factory = ctx.RequestServices.GetRequiredService<ProblemDetailsFactory>();

                // by using custom mapping, ex.Message is preserved as detail on the response in production mode.
                return factory.CreateProblemDetails(ctx, StatusCodes.Status409Conflict, detail: ex.Message);

            });
        });
        services.AddFluentValidationAutoValidation();
        services.AddScoped<IGuidGenerator, GuidGenerator>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        // Application
        services.AddValidatorsFromAssemblyContaining<CreateCompanyDtoValidator>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ICompanyService, CompanyService>();

        // Domain
        services.AddScoped<ICompanyManager, CompanyManager>();
        services.AddScoped<IEmployeeManager, EmployeeManager>();

        // Data + Domain
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Data
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

