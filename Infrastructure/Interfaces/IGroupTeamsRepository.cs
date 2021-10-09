using System;
using Infrastructure.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
    public interface IGroupTeamsRepository
    {
        Task<bool> AddGroupDetailsAsync(GroupTeamEntity groupDetail);
        Task<bool> DeleteGroupDetailsAsync(GroupTeamEntity groupDetail);
        Task<GroupTeamEntity> GetGroupDetailsAsync(Guid id);
        Task<GroupTeamEntity> GetGroupDetailsByGroupAdnTeamAsync(Guid idGroup, Guid idTeam);
        Task<List<GroupTeamEntity>> GetGroupsDetailsByGroupAsync(Guid IdGroup);
        Task<GroupTeamEntity> GetGroupDetailsByTeamAsync(Guid teamId);
        Task<bool> UpdateGroupDetailsAsync(GroupTeamEntity groupDetail);
        Task<GroupTeamEntity> GetGroupDetailsByGroupAsync(Guid idGroup);
    }
}
