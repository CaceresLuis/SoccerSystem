using Microsoft.AspNetCore.Http;

namespace Web.ViewModel
{
    public class TeamViewModel
    {
        public int Id { get; set; }
        public IFormFile LogoFile { get; set; }
        public string LogoPath { get; set; }
        public string Name { get; set; }
    }
}
