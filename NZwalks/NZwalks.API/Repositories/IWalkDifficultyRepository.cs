﻿using NZwalks.API.Models.Domain;

namespace NZwalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllWalksDifficultyAsync();
        Task<WalkDifficulty> GetWalkDifficultyAsync(Guid id);
        Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id, WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> DeleteWalkDifficultyAsync(Guid id);
    }
}
