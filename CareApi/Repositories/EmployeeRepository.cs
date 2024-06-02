using CareApi.Models;
using CareApi.Repositories.Abstraction;
using System.Text.Json;

namespace CareApi.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    public IEnumerable<EmployeeModel> GetEmployees()
    {
        return JsonSerializer.Deserialize<List<EmployeeModel>>(File.ReadAllText("../Data/employees.json"))!;
    }
}