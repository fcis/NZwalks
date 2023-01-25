using NZwalks.API.Models.Domain;

namespace NZwalks.API.Repositories
{
    public interface IRegionRepository
    {
       Task< IEnumerable<Region>> GetAllAsync();
       Task<Region> GetRegionAsync(Guid id);
       Task<Region> AddRegionAsync(Region region);
       Task<Region> DeleteRegionAsync(Guid id);
        Task<Region> UpdateRegionAsync(Guid id,Region region);
    }
}
