using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure
{
    public class DataContext : IdentityDbContext<UserEntity>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

       public DbSet<TeamEntity> Teams { get; set; }
       public DbSet<ImageEntity> Images { get; set; }
       public DbSet<GroupEntity> Groups { get; set; }
       public DbSet<MatchEntity> Matchs { get; set; }
       public DbSet<GroupTeamEntity> GroupTeams { get; set; }
       public DbSet<TournamentEntity> Tournaments { get; set; }
    }
}
