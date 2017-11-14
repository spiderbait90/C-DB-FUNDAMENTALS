using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data
{
    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext()
        {
        }

        public FootballBettingContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(Configuration.ConfigurationString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Color>()
                .HasMany(x => x.PrimaryKitTeams)
                .WithOne(x => x.PrimaryKitColor)
                .HasForeignKey(x => x.PrimaryKitColorId);

            builder.Entity<Color>()
                .HasMany(x => x.SecondaryKitTeams)
                .WithOne(x => x.SecondaryKitColor)
                .HasForeignKey(x => x.SecondaryKitColorId);

            builder.Entity<Town>()
                .HasMany(x => x.Teams)
                .WithOne(x => x.Town)
                .HasForeignKey(x => x.TownId);

            builder.Entity<Town>()
                .HasMany(x => x.Teams)
                .WithOne(x => x.Town)
                .HasForeignKey(x => x.TownId);

            builder.Entity<Team>()
                .HasMany(x => x.HomeGames)
                .WithOne(x => x.HomeTeam)
                .HasForeignKey(x => x.HomeTeamId);

            builder.Entity<Team>()
                .HasMany(x => x.AwayGames)
                .WithOne(x => x.AwayTeam)
                .HasForeignKey(x => x.AwayTeamId);

            builder.Entity<Country>()
                .HasMany(x => x.Towns)
                .WithOne(x => x.Country)
                .HasForeignKey(x => x.CountryId);

            builder.Entity<Team>()
                .HasMany(x => x.Players)
                .WithOne(x => x.Team)
                .HasForeignKey(x => x.TeamId);

            builder.Entity<Position>()
                .HasMany(x => x.Players)
                .WithOne(x => x.Position)
                .HasForeignKey(x => x.PositionId);

            builder.Entity<Game>()
                .HasMany(x => x.Bets)
                .WithOne(x => x.Game)
                .HasForeignKey(x => x.GameId);

            builder.Entity<User>()
                .HasMany(x => x.Bets)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.Entity<User>()
                .HasMany(x => x.Bets)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.Entity<PlayerStatistic>()
                .HasKey(x => new { x.GameId, x.PlayerId });

            builder.Entity<PlayerStatistic>()
                .HasOne(x => x.Game)
                .WithMany(x => x.PlayerStatistics)
                .HasForeignKey(x => x.GameId);

            builder.Entity<PlayerStatistic>()
                .HasOne(x => x.Player)
                .WithMany(x => x.PlayerStatistics)
                .HasForeignKey(x => x.PlayerId);

            builder.Entity<Bet>()
                .Property(x => x.Prediction).IsRequired(true);
        }
    }
}
