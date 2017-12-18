﻿using System.Xml.Linq;
using Instagraph.Models;
using Microsoft.EntityFrameworkCore;

namespace Instagraph.Data
{
    public class InstagraphContext : DbContext
    {
        public InstagraphContext() { }

        public InstagraphContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserFollower> UsersFollowers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserFollowerConfig());

            modelBuilder.ApplyConfiguration(new UserConfig());

            modelBuilder.ApplyConfiguration(new PostConfig());

            modelBuilder.ApplyConfiguration(new PictureConfig());

            modelBuilder.ApplyConfiguration(new CommentConfig());
        }
    }
}
