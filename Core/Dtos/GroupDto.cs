using System;

namespace Core.Dtos
{
    public class GroupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public TournamentDto Tournament { get; set; }
    }
}
