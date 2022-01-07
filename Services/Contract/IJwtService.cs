
using System.Threading.Tasks;
using Data.Entities.User;

namespace Services.Contract
{
    public interface IJwtService
    {
        Task<AccessToken> GenerateAsync(User user);
        string GetTest();
    }
}