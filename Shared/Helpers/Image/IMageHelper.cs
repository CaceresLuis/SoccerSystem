using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shared.Helpers.Image
{
    public class IMageHelper : IIMageHelper
    {
        public async Task<string> UploadImageAsync(IFormFile imageFile, string folder)
        {
            string file = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            string path = Path.Combine(
                Directory.GetCurrentDirectory(),
                $"wwwroot\\images\\{folder}",
                file);

            if (path.Contains("Api"))
                path = path.Replace("Api", "Web");

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"/images/{folder}/{file}";
        }

        public void DeleteImage(string path)
        {
            var p = path.Replace("/", "\\");
            string route = Path.Combine(
                Directory.GetCurrentDirectory(),
                $"wwwroot{p}");

            if (route.Contains("Api"))
                route = route.Replace("Api", "Web");

            File.Delete(route);
        }
    }
}
