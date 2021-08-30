using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<TournamentEntity> GetTournamentFindAsync(int id)
        {
            return await _dataContext.Tournaments.FindAsync(id);
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
            .ThenInclude(g => g.GroupDetails)
            .ThenInclude(gd => gd.Team)
            .Include(t => t.Groups)
            .ThenInclude(g => g.Matches)
            .ThenInclude(m => m.Local)
            .Include(t => t.Groups)
            .ThenInclude(g => g.Matches)
            .ThenInclude(m => m.Visitor)
            .ToArrayAsync());
        }

        public async Task<TournamentEntity> GetTournamentDetailsAsync(int id)
        {
            return await _dataContext.Tournaments
                .Include(t => t.Groups)
                .ThenInclude(t => t.Matches)
                .ThenInclude(t => t.Local)
                .Include(t => t.Groups)
                .ThenInclude(t => t.Matches)
                .ThenInclude(t => t.Visitor)
                .Include(t => t.Groups)
                .ThenInclude(t => t.GroupDetails)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public Task<bool> UpdateTournamentAsync(TournamentEntity tournament)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTournamentAsync(TournamentEntity tournament)
        {
            throw new NotImplementedException();
        }
    }
}
