using System.Linq;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GroupDetailsRepository : IGroupDetailsRepository
    {
        private readonly DataContext _dataContext;

        public GroupDetailsRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> AddGroupDetailsAsync(GroupDetailEntity groupDetail)
        {
            _dataContext.GroupDetails.Add(groupDetail);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<List<GroupDetailEntity>> GetGroupsDetailsByGroupAsync(int idGroup)
        {
            return await _dataContext.GroupDetails.Where(gd => gd.Group.Id == idGroup).ToListAsync();
        }
        public async Task<GroupDetailEntity> GetGroupDetailsByGroupAsync(int idGroup)
        {
            return await _dataContext.GroupDetails.FirstOrDefaultAsync(gb => gb.Group.Id == idGroup);
        }

        public async Task<GroupDetailEntity> GetGroupDetailsByTeamAsync(int teamId)
        {
            return await _dataContext.GroupDetails.FirstOrDefaultAsync(gd => gd.Team.Id == teamId);
        }

        public async Task<GroupDetailEntity> GetGroupDetailsAsync(int id)
        {
            return await _dataContext.GroupDetails
                .Include(gd => gd.Team)
                .Include(gd => gd.Group)
                .ThenInclude(g => g.Tournament)
                .FirstOrDefaultAsync(gd => gd.Id == id);
        }

        public async Task<bool> GetGroupDetailsByGroupAdnTeamAsync(int idGroup, int idTeam)
        {
            return await _dataContext.GroupDetails.AnyAsync(gd => gd.Team.Id == idTeam && gd.Group.Id == idGroup);
        }

        public async Task<bool> UpdateGroupDetailsAsync(GroupDetailEntity groupDetail)
        {
            _dataContext.GroupDetails.Update(groupDetail);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteGroupDetailsAsync(GroupDetailEntity groupDetail)
        {
            _dataContext.GroupDetails.Remove(groupDetail);
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
