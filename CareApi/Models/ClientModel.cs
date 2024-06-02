namespace CareApi.Models;

public class ClientModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime BirthDate { get; set; }
    public required string Gender { get; set; }
    public required string Id { get; set; }
}
