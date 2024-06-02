using CareApi.Models;
using CareApi.Repositories.Abstraction;

namespace CareApi.Services;

public class AuthEmployeeService(HashingService hashingService, IEmployeeRepository employeeRepository)
{
    private readonly HashingService hashingService = hashingService;
    private readonly IEmployeeRepository employeeRepository = employeeRepository;

    public EmployeeModel? ValidateToken(HttpContext httpContext)
    {
        //TODO add time component (expiration) to token.
        string authHeader = httpContext.Request.Headers.Authorization!;
        if (authHeader == null || !authHeader.StartsWith("Token "))
        {
            return null;
        }
        var token = authHeader.Substring("Token ".Length).Trim();

        var employees = employeeRepository.GetEmployees();
        var validEmployee = employees.FirstOrDefault(e => hashingService.HashWithSecretKey(e.EmployeeId) == token);

        return validEmployee;
    }
}