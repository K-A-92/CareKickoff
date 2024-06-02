namespace CareApi.Models;

public class CarePlanModel
{
    public required string Id { get; set; }
    public required string DisplayName { get; set; }
    public required string ClientId { get; set; }
    public List<GoalModel> Goals { get; set; } = [];
}
