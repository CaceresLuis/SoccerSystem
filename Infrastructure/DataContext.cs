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

       public DbSet<GroupTeamEntity> GroupTeams { get; set; }
       public DbSet<GroupEntity> Groups { get; set; }
       public DbSet<MatchEntity> Matchs { get; set; }
       public DbSet<PredictionEntity> Predictions { get; set; }
       public DbSet<TeamEntity> Teams { get; set; }
       public DbSet<TournamentEntity> Tournaments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Los nombres de los teams seran unicos
            builder.Entity<TeamEntity>()
                .HasIndex(t => t.Name)
                .IsUnique();
        }
    }
}
