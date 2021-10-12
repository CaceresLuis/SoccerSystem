using Core.Dtos.DtosApi;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public interface IResetMatchHelper
    {
        Task<bool> ResetMatchAsync(CloseMatchDto closeMatchDto);
    }
}
