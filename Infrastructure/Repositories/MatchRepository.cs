using System.Linq;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly DataContext _dataContext;

        public MatchRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> AddMatchAsync(MatchEntity match)
        {
            _dataContext.Matchs.Add(match);

            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<List<MatchEntity>> GetMatchByGroupAsync(int idGroup)
        {
            return await _dataContext.Matchs
                .Include(m => m.Group)
                .Include(m => m.Visitor)
                .Include(m => m.Local)
                .Where(m => m.Group.Id == idGroup)
                .ToListAsync();
        }

        public async Task<MatchEntity> GetMatchAsync(int id)
        {
            var a = await _dataContext.Matchs
                .Include(m => m.Group)
                .Include(m => m.Visitor)
                .Include(m => m.Local)
                .FirstOrDefaultAsync(m => m.Id == id);
            return a;
        }

        public async Task<MatchEntity> FindMatchByIdAsync(int matchId)
        {
            return await _dataContext.Matchs.FindAsync(matchId);
        }

        public async Task<bool> UpdateMatchAsync(MatchEntity match)
        {
            _dataContext.Update(match);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteMatchAsync(MatchEntity match)
        {
            _dataContext.Remove(match);
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
