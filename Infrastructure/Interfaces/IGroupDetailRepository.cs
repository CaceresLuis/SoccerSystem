using Infrastructure.Models;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IGroupDetailRepository
    {
        Task<GroupDetailEntity> GetGroupDetailsAsync(int id);
    }
}