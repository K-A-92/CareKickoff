using CareApi.Models;
using CareApi.Repositories.Abstraction;
using CareApi.Services;
using Microsoft.OpenApi.Models;

namespace CareApi.Endpoints;

public static class EmployeeEndpoints
{
    // TODO move authorization to middleware
    // TODO move "business logic" to own service
    public static void AddEmployeeEndpoints(this IEndpointRouteBuilder app)
    {
        var employeePath = app.MapGroup("/Employee");

        employeePath
            .MapGet("/GetClients", GetClients)
            .Produces<IEnumerable<ClientModel>>()
            .ProducesProblem(424)
            .WithOpenApi(op => new OpenApiOperation(op)
            {
                Summary = "Gets Clients",
                Description = ""
            });

        employeePath
            .MapGet("/GetCarePlans", GetCarePlans)
            .Produces<IEnumerable<CarePlanModel>>()
            .ProducesProblem(424)
            .WithOpenApi(op => new OpenApiOperation(op)
            {
                Summary = "Gets Careplans",
                Description = ""
            });

        employeePath
            .MapGet("/GetReports", GetReports)
            .Produces<IEnumerable<ReportModel>>()
            .ProducesProblem(424)
            .WithOpenApi(op => new OpenApiOperation(op)
            {
                Summary = "Gets Reports",
                Description = ""
            });
    }

    public static IResult GetClients(
        HttpContext httpContext,
        AuthEmployeeService authEmployeeService,
        IClientRepository clientRepository)
    {
        var validEmployee = authEmployeeService.GetValidEmployee(httpContext);
        if (validEmployee == null)
        {
            return Results.Unauthorized();
        }
        var authorizedClients = clientRepository.GetClients().Where(c => validEmployee.AuthorizedClients.Contains(c.Id)).ToArray();
        return Results.Ok(authorizedClients);
    }

    public static IResult GetCarePlans(
        HttpContext httpContext,
        AuthEmployeeService authEmployeeService,
        IClientRepository clientRepository,
        ICarePlanRepository carePlanRepository)
    {
        var validEmployee = authEmployeeService.GetValidEmployee(httpContext);
        if (validEmployee == null)
        {
            return Results.Unauthorized();
        }
        var clients = clientRepository.GetClients();
        var authorizedClientsIds = clients!.Where(c => validEmployee.AuthorizedClients.Contains(c.Id)).Select(c => c.Id);

        var carePlans = carePlanRepository.GetCarePlans();
        var applicableCarePlans = carePlans!.Where(cp => authorizedClientsIds.Contains(cp.ClientId));

        return Results.Ok(applicableCarePlans);
    }

    public static IResult GetReports(
        HttpContext httpContext,
        AuthEmployeeService authEmployeeService,
        IClientRepository clientRepository,
        IReportRepository reportRepository)
    {
        var validEmployee = authEmployeeService.GetValidEmployee(httpContext);
        if (validEmployee == null)
        {
            return Results.Unauthorized();
        }
        var clients = clientRepository.GetClients();
        var authorizedClientsIds = clients!.Where(c => validEmployee.AuthorizedClients.Contains(c.Id)).Select(c => c.Id);

        var reports = reportRepository.GetReports();
        var applicableReports = reports!.Where(r => authorizedClientsIds.Contains(r.ClientId));

        return Results.Ok(applicableReports);
    }
}