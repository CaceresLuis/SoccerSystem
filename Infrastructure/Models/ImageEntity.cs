using System;

namespace Infrastructure.Models
{
    public class ImageEntity
    {
        public int Id { get; set; }
        public Guid Reference { get; set; }
        public string Path { get; set; }
    }
}
