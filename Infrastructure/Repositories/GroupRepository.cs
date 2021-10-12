using System.Linq;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace Infrastructure.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DataContext _dataContext;

        public GroupRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> AddGroupAsync(GroupEntity group)
        {
            TournamentEntity tournament = await _dataContext.Tournaments.FindAsync(group.Tournament.Id);
            group.Tournament = tournament;
            _dataContext.Add(group);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<GroupEntity> FindGroupByIdAsync(Guid id)
        {
            return await _dataContext.Groups.FindAsync(id);
        }

        public async Task<GroupEntity> GetGroupWithTournamentAsync(Guid id)
        {
            return await _dataContext.Groups.Include(g => g.Tournament).FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<GroupEntity[]> GetListGroupWithTournamentAsync()
        {
            return await _dataContext.Groups.Include(g => g.Tournament).ToArrayAsync();
        }

        public async Task<GroupEntity> GetGroupByNameAndTournamentAsync(Guid idTournament, string groupName)
        {
            return await _dataContext.Groups.Include(g => g.Tournament)
                .Where(g => g.Tournament.Id == idTournament && g.Name == groupName).FirstOrDefaultAsync();
        }

        public async Task<List<GroupEntity>> GetAllGroupOfTournamentAsync(Guid idTournamnet)
        {
            return await _dataContext.Groups.Include(g => g.Tournament).Where(g => g.Tournament.Id == idTournamnet && g.IsActive == true).ToListAsync();
        }

        public async Task<GroupEntity> GetFullGroupAsync(Guid id)
        {
            return await _dataContext.Groups
                .Include(g => g.GroupTeams)
                .ThenInclude(gt => gt.Team)
                .Include(g => g.Matches)
                .ThenInclude(g => g.Local)
                .Include(g => g.Matches)
                .ThenInclude(g => g.Visitor)
                .Include(g => g.Tournament)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<GroupEntity> GetGroupTeamAndDetailsAsync(Guid id)
        {
            return await _dataContext.Groups
                .Include(g => g.Tournament)
                .Include(g => g.GroupTeams)
                .ThenInclude(gd => gd.Team)
                .Include(g => g.Matches)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<List<GroupEntity>> GetGroupTeamTournamentsAsync()
        {
            return await _dataContext.Groups
                .Include(g => g.Tournament)
                .Include(g => g.GroupTeams)
                .ThenInclude(gd => gd.Team)
                .Include(g => g.Matches)
                .Where(g => g.Tournament.IsActive == true)
                .ToListAsync();
        }

        public async Task<bool> UpdateGroupAsync(GroupEntity group)
        {
            _dataContext.Groups.Update(group);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteGroupAsync(GroupEntity group)
        {
            _dataContext.Groups.Remove(group);
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
