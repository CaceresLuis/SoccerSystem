using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class GroupEntity
    {
        public int Id { get; set; }

        [MaxLength(30, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public TournamentEntity Tournament { get; set; }
        public ICollection<MatchEntity> Matches { get; set; }
        public ICollection<GroupTeamEntity> GroupTeams { get; set; }
    }
}
