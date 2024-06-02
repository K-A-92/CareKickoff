using CareApi.Models;

namespace CareApi.Repositories.Abstraction
{
    public interface IReportRepository
    {
        IEnumerable<ReportModel> GetReports();
    }
}