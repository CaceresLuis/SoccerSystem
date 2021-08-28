using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly DataContext _dataContext;

        public TeamRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> AddTeamAsync(TeamEntity team)
        {
            _dataContext.Teams.Add(team);

            //SaveChangesAsync devuelve un int si es mayor que CERO devuelve true
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<TeamEntity> FindTeamByIdAsync(int teamId)
        {
            return await _dataContext.Teams.FindAsync(teamId);
        }

        public async Task<TeamEntity> FindTeamByNameAsync(string teamName)
        {
            return await _dataContext.Teams.FirstOrDefaultAsync(t => t.Name == teamName) ;
        }

        public async Task<TeamEntity[]> GetAllTeamAsync()
        {
            return await _dataContext.Teams.ToArrayAsync();
        }

        public async Task<bool> UpdateTeamAsync(TeamEntity team)
        {
            _dataContext.Update(team);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTeamAsync(TeamEntity team)
        {
            _dataContext.Remove(team);
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
