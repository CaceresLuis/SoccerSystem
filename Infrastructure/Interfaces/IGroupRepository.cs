using System;
using Infrastructure.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
    public interface IGroupRepository
    {
        Task<bool> AddGroupAsync(GroupEntity group);
        Task<bool> DeleteGroupAsync(GroupEntity group);
        Task<GroupEntity> FindGroupByIdAsync(Guid id);
        Task<GroupEntity> GetGroupByNameAndTournamentAsync(Guid idTournament, string groupName);
        Task<List<GroupEntity>> GetAllGroupOfTournamentAsync(Guid idTournamnet);
        Task<bool> UpdateGroupAsync(GroupEntity group);
        Task<GroupEntity> GetGroupWithTournamentAsync(Guid id);
        Task<GroupEntity> GetFullGroupAsync(Guid id);
        Task<GroupEntity> GetGroupTeamAndDetailsAsync(Guid id);
        Task<List<GroupEntity>> GetGroupTeamTournamentsAsync();
        Task<GroupEntity[]> GetListGroupWithTournamentAsync();
    }
}
