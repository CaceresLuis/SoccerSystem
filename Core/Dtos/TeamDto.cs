using Microsoft.AspNetCore.Http;

namespace Core.Dtos
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile LogoFile { get; set; }
        public string LogoPath { get; set; }
    }
}
