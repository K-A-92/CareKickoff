using CareApi.Endpoints;
using CareApi.Repositories;
using CareApi.Repositories.Abstraction;
using CareApi.Services;

namespace CareApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddAuthorization();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<IAuthRepository, AuthRepository>();
        builder.Services.AddSingleton<IClientRepository, ClientRepository>();
        builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddSingleton<IReportRepository, ReportRepository>();
        builder.Services.AddSingleton<ICarePlanRepository, CarePlanRepository>();
        builder.Services.AddSingleton<AuthEmployeeService>();
        builder.Services.AddSingleton<HashingService>();

        //Since this is a local application:
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigin",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("AllowAllOrigin");

        var authService = app.Services.GetRequiredService<AuthEmployeeService>();
        var clientRepository = app.Services.GetRequiredService<IClientRepository>();
        var carePlanRepository = app.Services.GetRequiredService<ICarePlanRepository>();
        var reportRepository = app.Services.GetRequiredService<IReportRepository>();
        app.AddEmployeeEndpoints(authService, clientRepository, carePlanRepository, reportRepository);

        var hashingService = app.Services.GetRequiredService<HashingService>();
        var authRepository = app.Services.GetRequiredService<IAuthRepository>();
        app.AddLoginEndpoint(hashingService, authRepository);

        app.Run();
    }
}
