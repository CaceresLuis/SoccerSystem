using System;
using System.Linq;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly DataContext _dataContext;

        public TournamentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> AddTournamentAsync(TournamentEntity tournament)
        {
            _dataContext.Tournaments.Add(tournament);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<TournamentEntity> GetTournamentFindAsync(Guid id)
        {
            return await _dataContext.Tournaments.FindAsync(id);
        }
        public async Task<TournamentEntity> GetTournamentByNameAsync(string name)
        {
            return await _dataContext.Tournaments.FirstOrDefaultAsync(t => t.Name == name);
        }
        
        public async Task<TournamentEntity> GetTournamentWithGroupAsync(Guid id)
        {
            return await _dataContext.Tournaments
                .Include(t => t.Groups).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TournamentEntity> GetTournamentDetailsAsync(Guid id)
        {
            return await _dataContext.Tournaments
                .Include(t => t.Groups)
                .ThenInclude(t => t.Matches)
                .ThenInclude(t => t.Local)
                .Include(t => t.Groups)
                .ThenInclude(t => t.Matches)
                .ThenInclude(t => t.Visitor)
                .Include(t => t.Groups)
                .ThenInclude(t => t.GroupTeams)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TournamentEntity[]> GetTournamentsAsync()
        {
            return await _dataContext.Tournaments
                .Include(t => t.Groups).OrderBy(t => t.StartDate).ToArrayAsync();
        }

        public async Task<TournamentEntity[]> GetTournamentsDetailsAsync()
        {
            return (await _dataContext.Tournaments
            .Include(t => t.Groups)
            .ThenInclude(g => g.GroupTeams)
            .ThenInclude(gd => gd.Team)
            .Include(t => t.Groups)
            .ThenInclude(g => g.Matches)
            .ThenInclude(m => m.Local)
            .Include(t => t.Groups)
            .ThenInclude(g => g.Matches)
            .ThenInclude(m => m.Visitor)
            .ToArrayAsync());
        }

        public async Task<bool> UpdateTournamentAsync(TournamentEntity tournament)
        {
            _dataContext.Tournaments.Update(tournament);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTournamentAsync(TournamentEntity tournament)
        {
            _dataContext.Tournaments.Remove(tournament);
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
