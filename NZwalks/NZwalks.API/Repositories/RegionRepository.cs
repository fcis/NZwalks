using Microsoft.EntityFrameworkCore;
using NZwalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZwalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZwalksDbContext nZwalksDbContext;

        public RegionRepository(NZwalksDbContext nZwalksDbContext)
        {
            this.nZwalksDbContext = nZwalksDbContext;
        }
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await nZwalksDbContext.Regions.ToListAsync();
        }
    }
}
