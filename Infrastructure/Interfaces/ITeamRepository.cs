using Infrastructure.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
    public interface ITeamRepository
    {
        Task<bool> AddTeamAsync(TeamEntity team);
        Task<TeamEntity> FindTeamByIdAsync(int teamId);
        Task<TeamEntity> FindTeamByNameAsync(string teamName);
        Task<List<TeamEntity>> GetAllTeamAsync();
        //List<TeamEntity> GetAllTeamAsync();
        Task<bool> UpdateTeamAsync(TeamEntity team);
        Task<bool> DeleteTeamAsync(TeamEntity team);
    }
}
