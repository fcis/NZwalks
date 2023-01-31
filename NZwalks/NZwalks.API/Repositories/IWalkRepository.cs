using NZwalks.API.Models.Domain;

namespace NZwalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllWalksAsync();
        Task<Walk> GetWalkAsync(Guid id);
        Task<Walk> AddWalkAsync(Walk walk);
        Task <Walk> UpdateWalkAsync(Guid id,Walk walk);
        Task<Walk> DeleteWalkAsync(Guid id);
    }
}
