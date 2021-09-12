using Infrastructure.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
    public interface IGroupDetailsRepository
    {
        Task<bool> AddGroupDetailsAsync(GroupDetailEntity groupDetail);
        Task<bool> DeleteGroupDetailsAsync(GroupDetailEntity groupDetail);
        Task<GroupDetailEntity> GetGroupDetailsAsync(int id);
        Task<bool> GetGroupDetailsByGroupAdnTeamAsync(int idGroup, int idTeam);
        Task<List<GroupDetailEntity>> GetGroupsDetailsByGroupAsync(int IdGroup);
        Task<GroupDetailEntity> GetGroupDetailsByTeamAsync(int teamId);
        Task<bool> UpdateGroupDetailsAsync(GroupDetailEntity groupDetail);
        Task<GroupDetailEntity> GetGroupDetailsByGroupAsync(int idGroup);
    }
}
