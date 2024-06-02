using CareApi.Models;
using CareApi.Repositories.Abstraction;
using CareApi.Services;
using Microsoft.OpenApi.Models;

namespace CareApi.Endpoints;

public static class EmployeeEndpoints
{
    // TODO move authorization to middleware
    // TODO move "business logic" to own service
    public static void AddEmployeeEndpoints(this IEndpointRouteBuilder app,
        AuthEmployeeService authEmployeeService,
        IClientRepository clientRepository,
        ICarePlanRepository carePlanRepository,
        IReportRepository reportRepository)
    {
        var employeePath = app.MapGroup("/Employee");

        employeePath
            .MapGet("/GetClients", (HttpContext httpContext) =>
            {
                var validEmployee = authEmployeeService.ValidateToken(httpContext);
                if (validEmployee == null)
                {
                    return Results.Unauthorized();
                }

                var authorizedClients = clientRepository.GetClients().Where(c => validEmployee.AuthorizedClients.Contains(c.Id)).ToArray();

                return Results.Ok(authorizedClients);
            })
            .Produces<IEnumerable<ClientModel>>()
            .ProducesProblem(424)
            .WithOpenApi(op => new OpenApiOperation(op)
            {
                Summary = "Gets Clients",
                Description = ""
            });

        employeePath
            .MapGet("/GetCarePlans", (HttpContext httpContext) =>
            {
                var validEmployee = authEmployeeService.ValidateToken(httpContext);
                if (validEmployee == null)
                {
                    return Results.Unauthorized();
                }
                var clients = clientRepository.GetClients();
                var authorizedClientsIds = clients!.Where(c => validEmployee.AuthorizedClients.Contains(c.Id)).Select(c => c.Id);

                var carePlans = carePlanRepository.GetCarePlans();
                var applicableCarePlans = carePlans!.Where(cp => authorizedClientsIds.Contains(cp.ClientId));

                return Results.Ok(applicableCarePlans);
            })
            .Produces<IEnumerable<CarePlanModel>>()
            .ProducesProblem(424)
            .WithOpenApi(op => new OpenApiOperation(op)
            {
                Summary = "Gets Careplans",
                Description = ""
            });

        employeePath
            .MapGet("/GetReports", (HttpContext httpContext) =>
            {
                var validEmployee = authEmployeeService.ValidateToken(httpContext);
                if (validEmployee == null)
                {
                    return Results.Unauthorized();
                }

                var carePlans = carePlanRepository.GetCarePlans();
                var clients = clientRepository.GetClients();
                var reports = reportRepository.GetReports();

                var authorizedClientsIds = clients!.Where(c => validEmployee.AuthorizedClients.Contains(c.Id)).Select(c => c.Id);
                var applicableReports = reports!.Where(r => authorizedClientsIds.Contains(r.ClientId));

                return Results.Ok(applicableReports);
            })
            .Produces<IEnumerable<ReportModel>>()
            .ProducesProblem(424)
            .WithOpenApi(op => new OpenApiOperation(op)
            {
                Summary = "Gets Reports",
                Description = ""
            });
    }
}