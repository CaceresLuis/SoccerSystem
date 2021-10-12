using System;
using Microsoft.AspNetCore.Http;

namespace Shared.Helpers.Image
{
    public class ImageData
    {
        public IFormFile File { get; set; }
        public Guid Reference { get; set; }
        public string Folder { get; set; }
    }
}
