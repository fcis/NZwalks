using NZwalks.API.Models.Domain;

namespace NZwalks.API.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}
