using Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IMatchRepository
    {
        Task<bool> AddMatchAsync(MatchEntity match);
        Task<bool> DeleteMatchAsync(MatchEntity match);
        Task<MatchEntity> FindMatchByIdAsync(int matchId);
        Task<MatchEntity> GetMatchAsync(int id);
        Task<List<MatchEntity>> GetMatchByGroupAsync(int idGroup);
        Task<bool> UpdateMatchAsync(MatchEntity match);
    }
}
