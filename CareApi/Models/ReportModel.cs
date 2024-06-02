namespace CareApi.Models;

public class ReportModel
{
    public required string Subject { get; set; }
    public required string Text { get; set; }
    public required bool HasPriority { get; set; }
    public required string CarePlanGoalId { get; set; }
    public required string ClientId { get; set; }
    public required string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}
