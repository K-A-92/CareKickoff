namespace CareApi.Models;

public class EmployeeModel
{
    public required string Name { get; set; }
    public required string EmployeeId { get; set; }
    public required List<string> AuthorizedClients { get; set; }
}
