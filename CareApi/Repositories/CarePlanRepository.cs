using CareApi.Models;
using CareApi.Repositories.Abstraction;
using System.Text.Json;

namespace CareApi.Repositories;

public class CarePlanRepository : ICarePlanRepository
{
    public IEnumerable<CarePlanModel> GetCarePlans()
    {
        return JsonSerializer.Deserialize<List<CarePlanModel>>(File.ReadAllText("../Data/careplans.json"))!;
    }
}