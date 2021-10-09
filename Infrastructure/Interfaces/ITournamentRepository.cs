using System;
using Infrastructure.Models;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ITournamentRepository
    {
        Task<bool> AddTournamentAsync(TournamentEntity tournament);
        Task<TournamentEntity> GetTournamentFindAsync(Guid id);
        Task<TournamentEntity[]> GetTournamentsAsync();
        Task<TournamentEntity[]> GetTournamentsDetailsAsync();
        Task<TournamentEntity> GetTournamentDetailsAsync(Guid id);
        Task<bool> UpdateTournamentAsync(TournamentEntity tournament);
        Task<bool> DeleteTournamentAsync(TournamentEntity tournament);
        Task<TournamentEntity> GetTournamentWithGroupAsync(Guid id);
        Task<TournamentEntity> GetTournamentByNameAsync(string name);
    }
}