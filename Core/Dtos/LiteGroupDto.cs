using System;

namespace Core.Dtos
{
    public class LiteGroupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Guid TournamentId { get; set; }
    }
}
