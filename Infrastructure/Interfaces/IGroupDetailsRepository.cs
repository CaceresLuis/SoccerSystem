using Infrastructure.Models;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IGroupDetailsRepository
    {
        Task<GroupDetailEntity> GetGroupDetailsAsync(int IdGroup);
    }
}
