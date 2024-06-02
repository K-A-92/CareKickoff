using CareApi.Models;

namespace CareApi.Repositories.Abstraction
{
    public interface ICarePlanRepository
    {
        IEnumerable<CarePlanModel> GetCarePlans();
    }
}