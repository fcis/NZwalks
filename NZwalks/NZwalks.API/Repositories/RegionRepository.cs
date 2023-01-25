using Microsoft.EntityFrameworkCore;
using NZwalks.API.Data;
using NZwalks.API.Models.Domain;

namespace NZwalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZwalksDbContext nZwalksDbContext;

        public RegionRepository(NZwalksDbContext nZwalksDbContext)
        {
            this.nZwalksDbContext = nZwalksDbContext;
        }

        public async Task<Region> AddRegionAsync(Region region)
        {
            region.Id = new Guid();
            await nZwalksDbContext.Regions.AddAsync(region);
            await nZwalksDbContext.SaveChangesAsync();
            return region;

        }

        public async Task<Region> DeleteRegionAsync(Guid id)
        {
             var region = await nZwalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return null;
            }
            nZwalksDbContext?.Regions.Remove(region);
           await nZwalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await nZwalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetRegionAsync(Guid id)
        {
            return await nZwalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateRegionAsync(Guid id, Region region)
        {
            var Existingregion = await nZwalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (Existingregion == null)
            {
                return null;
            }
            Existingregion.Area = region.Area;
            Existingregion.Code = region.Code;
            Existingregion.Name = region.Name;
            Existingregion.Lat= region.Lat;
            Existingregion.Long = region.Long;
            Existingregion.Population = region.Population;
            await nZwalksDbContext.SaveChangesAsync();

            return Existingregion;
            

        }
    }
}
