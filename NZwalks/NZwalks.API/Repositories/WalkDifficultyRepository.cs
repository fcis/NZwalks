using Microsoft.EntityFrameworkCore;
using NZwalks.API.Data;
using NZwalks.API.Models.Domain;

namespace NZwalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZwalksDbContext nZwalksDbContext;

        public WalkDifficultyRepository(NZwalksDbContext nZwalksDbContext)
        {
            this.nZwalksDbContext = nZwalksDbContext;
        }
        public async Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = new Guid(); 
            await nZwalksDbContext.WalkDifficulty.AddAsync(walkDifficulty);
            await nZwalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteWalkDifficultyAsync(Guid id)
        {
            var walkDifficulty = await nZwalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if (walkDifficulty == null) { return null; }
             nZwalksDbContext.WalkDifficulty.Remove(walkDifficulty);
            await nZwalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllWalksDifficultyAsync()
        {
           List<WalkDifficulty> walkDifficulties= new List<WalkDifficulty>(); 
            walkDifficulties = await nZwalksDbContext.WalkDifficulty.ToListAsync();
            return walkDifficulties;
        }

        public async Task<WalkDifficulty> GetWalkDifficultyAsync(Guid id)
        {
            var walkDifficulty = await nZwalksDbContext.WalkDifficulty.FindAsync(id);    
            if (walkDifficulty == null) { return null; }
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var ExistingWlakDifficulty = await nZwalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x=> x.Id == id);
            if (ExistingWlakDifficulty == null) { return null; }
            ExistingWlakDifficulty.Code = walkDifficulty.Code;
            await nZwalksDbContext.SaveChangesAsync();
            return ExistingWlakDifficulty;
        }
    }
}
