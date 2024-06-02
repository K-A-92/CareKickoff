using CareApi.Models;
using CareApi.Repositories.Abstraction;
using System.Text.Json;

namespace CareApi.Repositories;

public class ClientRepository : IClientRepository
{
    public IEnumerable<ClientModel> GetClients()
    {
        return JsonSerializer.Deserialize<List<ClientModel>>(File.ReadAllText("../Data/clients.json"))!;
    }
}