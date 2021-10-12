using System;
using Microsoft.AspNetCore.Http;

namespace Core.Dtos.AddDtos
{
    public class AddTournamentDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public IFormFile LogoFile { get; set; }
    }
}
