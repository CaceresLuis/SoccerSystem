using System;
using System.Linq;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GroupTeamsRepository : IGroupTeamsRepository
    {
        private readonly DataContext _dataContext;

        public GroupTeamsRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> AddGroupDetailsAsync(GroupTeamEntity groupDetail)
        {
            _dataContext.GroupTeams.Add(groupDetail);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<List<GroupTeamEntity>> GetGroupsDetailsByGroupAsync(Guid idGroup)
        {
            return await _dataContext.GroupTeams.Where(gd => gd.Group.Id == idGroup).ToListAsync();
        }
        public async Task<GroupTeamEntity> GetGroupDetailsByGroupAsync(Guid idGroup)
        {
            return await _dataContext.GroupTeams.FirstOrDefaultAsync(gb => gb.Group.Id == idGroup);
        }

        public async Task<GroupTeamEntity> GetGroupDetailsByTeamAsync(Guid teamId)
        {
            return await _dataContext.GroupTeams.FirstOrDefaultAsync(gd => gd.Team.Id == teamId);
        }

        public async Task<GroupTeamEntity> GetGroupDetailsAsync(Guid id)
        {
            return await _dataContext.GroupTeams
                .Include(gd => gd.Team)
                .Include(gd => gd.Group)
                .ThenInclude(g => g.Tournament)
                .FirstOrDefaultAsync(gd => gd.Id == id);
        }

        public async Task<GroupTeamEntity> GetGroupDetailsByGroupAdnTeamAsync(Guid idGroup, Guid idTeam)
        {
            return await _dataContext.GroupTeams
                .Include(gt => gt.Team)
                .FirstOrDefaultAsync(gd => gd.Team.Id == idTeam && gd.Group.Id == idGroup);
        }

        public async Task<bool> UpdateGroupDetailsAsync(GroupTeamEntity groupDetail)
        {
            _dataContext.GroupTeams.Update(groupDetail);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteGroupDetailsAsync(GroupTeamEntity groupDetail)
        {
            _dataContext.GroupTeams.Remove(groupDetail);
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
