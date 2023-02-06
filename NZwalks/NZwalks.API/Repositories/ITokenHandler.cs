using NZwalks.API.Models.Domain;

namespace NZwalks.API.Repositories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
