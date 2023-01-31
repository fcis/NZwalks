using Microsoft.EntityFrameworkCore;
using NZwalks.API.Data;
using NZwalks.API.Models.Domain;

namespace NZwalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZwalksDbContext nZwalksDbContext;

        public WalkRepository(NZwalksDbContext nZwalksDbContext)
        {
            this.nZwalksDbContext = nZwalksDbContext;
        }

        public async Task<Walk> AddWalkAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await nZwalksDbContext.AddAsync(walk);
            await nZwalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllWalksAsync()
        {
            return await nZwalksDbContext.Walks.Include(a=>a.Region)
                .Include(d=>d.WalkDifficulty).ToListAsync();
        }

        public async Task<Walk> GetWalkAsync(Guid id)
        {
            return await nZwalksDbContext.Walks.Include(r => r.Region)
                .Include(d => d.WalkDifficulty).SingleOrDefaultAsync(a=>a.Id==id);
        }

        public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
            var ExistingWalkDomain =await nZwalksDbContext.Walks.SingleOrDefaultAsync(a=>a.Id==id);
            if (ExistingWalkDomain == null) { return null; }
            ExistingWalkDomain.Length = walk.Length;
            ExistingWalkDomain.Name = walk.Name;
            ExistingWalkDomain.RegionId = walk.RegionId;
            ExistingWalkDomain.WalkDifficultyId = walk.WalkDifficultyId;

             await nZwalksDbContext.SaveChangesAsync();
            return ExistingWalkDomain;
        }
        public async Task<Walk> DeleteWalkAsync(Guid id)
        {
            var deletedWalk = await nZwalksDbContext.Walks.FirstOrDefaultAsync(a => a.Id == id);
            if (deletedWalk == null) { return null; }
             nZwalksDbContext.Walks.Remove(deletedWalk);
            await nZwalksDbContext.SaveChangesAsync();
            return deletedWalk;

        }
    }
}
