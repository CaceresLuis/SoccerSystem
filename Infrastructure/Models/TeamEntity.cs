using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class TeamEntity
    {
        public Guid Id { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Name { get; set; }
        public ICollection<GroupTeamEntity> GroupDetails { get; set; }
        public ICollection<UserEntity> Users { get; set; }
    }
}
