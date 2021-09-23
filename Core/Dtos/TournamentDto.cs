using System;
using Microsoft.AspNetCore.Http;

namespace Core.Dtos
{
    public class TournamentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public string LogoPath { get; set; }
        public IFormFile LogoFile { get; set; }
    }
}
