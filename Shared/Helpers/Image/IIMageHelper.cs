using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Shared.Helpers.Image
{
    public interface IIMageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string folder);
    }
}