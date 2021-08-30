using Infrastructure.Models;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ITournamentRepository
    {
        Task<bool> AddTournamentAsync(TournamentEntity tournament);
        Task<TournamentEntity> GetTournamentFindAsync(int id);
        Task<TournamentEntity[]> GetTournamentsAsync();
        Task<TournamentEntity[]> GetTournamentsDetailsAsync();
        Task<TournamentEntity> GetTournamentDetailsAsync(int id);
        Task<bool> UpdateTournamentAsync(TournamentEntity tournament);
        Task<bool> DeleteTournamentAsync(TournamentEntity tournament);
    }
}