﻿using Infrastructure.Models;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IGroupRepository
    {
        Task<bool> AddGroupAsync(GroupEntity group);
        Task<bool> DeleteGroupAsync(GroupEntity group);
        Task<GroupEntity> FindGroupByIdAsync(int id);
        Task<GroupEntity> GetGroupByNameAndTournamentAsync(int idTournament, string groupName);
        Task<GroupEntity[]> GetAllGroupOfTournamentAsync(int idTournamnet);
        Task<bool> UpdateGroupAsync(GroupEntity group);
        Task<GroupEntity> GetGroupWithTournamentAsync(int id);
    }
}
