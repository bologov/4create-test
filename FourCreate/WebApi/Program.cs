using System.Reflection;
using System.Text.Json.Serialization;
using Application.Company;
using Application.Employee;

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

            var contractsAssembly = typeof(Application.Contracts.CreateCompanyDto).Assembly;
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

        InitialiseServices(builder.Services, builder.Configuration);

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

    private static void InitialiseServices(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddTransient<IEmployeeService, EmployeeService>();
        serviceCollection.AddTransient<ICompanyService, CompanyService>();
    }
}

