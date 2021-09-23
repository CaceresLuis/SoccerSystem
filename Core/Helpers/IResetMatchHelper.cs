using Core.Dtos;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public interface IResetMatchHelper
    {
        Task<bool> ResetMatchAsync(MatchDto matchDto);
    }
}
