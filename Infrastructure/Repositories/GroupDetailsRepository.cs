using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
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

        public async Task<GroupDetailEntity> GetGroupDetailsAsync(int IdGroup)
        {
            return await _dataContext.GroupDetails.FirstOrDefaultAsync(gd => gd.Group.Id == IdGroup);
        }
    }
}
