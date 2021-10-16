using System;
using Infrastructure.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
    public interface IMatchRepository
    {
        Task<bool> AddMatchAsync(MatchEntity match);
        Task<bool> ConfirmAvailability(MatchEntity matchEntity);
        Task<bool> DeleteMatchAsync(MatchEntity match);
        Task<MatchEntity> FindMatchByIdAsync(Guid matchId);
        Task<MatchEntity> GetMatchAsync(Guid id);
        Task<List<MatchEntity>> GetMatchByGroupAsync(Guid idGroup);
        Task<bool> UpdateMatchAsync(MatchEntity match);
    }
}
