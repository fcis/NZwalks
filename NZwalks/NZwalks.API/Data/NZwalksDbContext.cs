using Microsoft.EntityFrameworkCore;
using NZwalks.API.Models.Domain;

namespace NZwalks.API.Data
{
    public class NZwalksDbContext : DbContext
    {
        public NZwalksDbContext(DbContextOptions options) : base(options)
        {

        }
        public  DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }

    }
}
