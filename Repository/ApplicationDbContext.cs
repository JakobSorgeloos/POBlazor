using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;

using System.Text;

namespace Repository
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>

    {
        public DbSet<Media> MediaSites { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Comment
            builder.Entity<Comment>().HasOne(_ => _.Media).WithMany(_ => _.Comments);
            builder.Entity<Comment>().HasOne(_ => _.Gebruiker).WithMany(_ => _.Comments);

            //Playlist
            builder.Entity<Playlist>().HasOne(_ => _.User).WithOne(_ => _.LikedSongs);

        }

    }
}
