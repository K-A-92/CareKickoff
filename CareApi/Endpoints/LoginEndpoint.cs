using CareApi.Models;
using CareApi.Repositories.Abstraction;
using CareApi.Services;
using Microsoft.OpenApi.Models;

namespace CareApi.Endpoints;

public static class LoginEndpoint
{
    public static void AddLoginEndpoint(this IEndpointRouteBuilder app)
    {
        app
            .MapPost("/Authenticate", Authenticate)
            .Produces<string>()
            .ProducesProblem(424)
            .WithOpenApi(op => new OpenApiOperation(op)
            {
                Summary = "Login",
                Description = ""
            });
    }

    public static async Task<IResult> Authenticate(
        HttpContext httpContext,
        HashingService hashingService,
        IAuthRepository authRepository)
    {
        var loginModel = await httpContext.Request.ReadFromJsonAsync<LoginModel>();
        var auths = authRepository.GetAuths();
        var foundLogin = auths!.Where(e => e.UserName == loginModel!.Username && e.Password == loginModel.Password).SingleOrDefault();
        if (foundLogin == null)
        {
            return Results.NotFound();
        }

        var hashedToken = hashingService.HashWithSecretKey(foundLogin!.EmployeeId);
        return Results.Ok(hashedToken);
    }
}