using CareApi.Models;

namespace CareApi.Repositories.Abstraction
{
    public interface IAuthRepository
    {
        IEnumerable<AuthModel> GetAuths();
    }
}