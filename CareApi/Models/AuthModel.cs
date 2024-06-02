namespace CareApi.Models;

public class AuthModel
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string EmployeeId { get; set; }
}