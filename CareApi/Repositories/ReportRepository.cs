using CareApi.Models;
using CareApi.Repositories.Abstraction;
using System.Text.Json;

namespace CareApi.Repositories;

public class ReportRepository : IReportRepository
{
    public IEnumerable<ReportModel> GetReports()
    {
        return JsonSerializer.Deserialize<List<ReportModel>>(File.ReadAllText("../Data/reports.json"))!;
    }
}