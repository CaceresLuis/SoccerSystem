using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shared.Helpers.Image
{
    public interface IIMageHelper
    {
        void DeleteImage(string path);
        Task<string> UploadImageAsync(IFormFile imageFile, string folder);
    }
}