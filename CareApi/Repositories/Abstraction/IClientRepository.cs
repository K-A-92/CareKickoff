using CareApi.Models;

namespace CareApi.Repositories.Abstraction
{
    public interface IClientRepository
    {
        IEnumerable<ClientModel> GetClients();
    }
}