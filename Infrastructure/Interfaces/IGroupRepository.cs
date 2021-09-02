using Infrastructure.Models;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IGroupRepository 
    {
        Task<bool> AddGroupAsync(GroupEntity group);
        Task<GroupEntity> GetGroupDetailsAsync(int id);
        Task<GroupEntity> FindGroupAsync(int id);
        Task<GroupEntity> GetGroupTournamentsAsync(int id);
        Task<bool> UpdateGroupAsync(GroupEntity group);
        Task<bool> DeleteGroupAsync(GroupEntity group);
    }
}