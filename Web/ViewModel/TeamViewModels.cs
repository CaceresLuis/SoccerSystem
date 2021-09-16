using Microsoft.AspNetCore.Http;

namespace Web.ViewModel
{
    public class TeamViewModels
    {
        public int Id { get; set; }
        public IFormFile LogoFile { get; set; }
        public string LogoPath { get; set; }
        public string Name { get; set; }
    }
}
