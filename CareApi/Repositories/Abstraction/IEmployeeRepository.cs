using CareApi.Models;

namespace CareApi.Repositories.Abstraction
{
    public interface IEmployeeRepository
    {
        IEnumerable<EmployeeModel> GetEmployees();
    }
}