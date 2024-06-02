using CareApi.Models;
using CareApi.Repositories.Abstraction;
using System.Text.Json;

namespace CareApi.Repositories;

public class AuthRepository : IAuthRepository
{
    public IEnumerable<AuthModel> GetAuths()
    {
        return JsonSerializer.Deserialize<List<AuthModel>>(File.ReadAllText("../Data/auths.json"))!; //TODO dont store auth in plain text.
    }
}
