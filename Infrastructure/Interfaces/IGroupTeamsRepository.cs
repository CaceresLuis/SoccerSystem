using Infrastructure.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
    public interface IGroupTeamsRepository
    {
        Task<bool> AddGroupDetailsAsync(GroupTeamEntity groupDetail);
        Task<bool> DeleteGroupDetailsAsync(GroupTeamEntity groupDetail);
        Task<GroupTeamEntity> GetGroupDetailsAsync(int id);
        Task<GroupTeamEntity> GetGroupDetailsByGroupAdnTeamAsync(int idGroup, int idTeam);
        Task<List<GroupTeamEntity>> GetGroupsDetailsByGroupAsync(int IdGroup);
        Task<GroupTeamEntity> GetGroupDetailsByTeamAsync(int teamId);
        Task<bool> UpdateGroupDetailsAsync(GroupTeamEntity groupDetail);
        Task<GroupTeamEntity> GetGroupDetailsByGroupAsync(int idGroup);
    }
}
