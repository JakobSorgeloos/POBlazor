using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Text;

namespace Repository
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>

    {
        //public DbSet<Film> Films { get; set; }
        //public DbSet<Muziek> Muziek { get; set; }
        //public DbSet<Serie> Series { get; set; }
        //public DbSet<Podcast> Podcasts { get; set; }
        public DbSet<Review> Reviews { get; set; }
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



            //MediaSite
            builder.Entity<Film>();
            builder.Entity<Music>();
            builder.Entity<Podcast>();
            builder.Entity<Serie>();
            builder.Entity<Media>().ToTable("Media").HasMany(c => c.Playlists).WithMany(x => x.MediaSites);

            //Review
            builder.Entity<Review>().HasOne(_ => _.Media).WithMany(_ => _.Review);
            builder.Entity<Review>().HasOne(_ => _.Gebruiker).WithMany(_ => _.Reviews);

            //Comment
            builder.Entity<Comment>().HasOne(_ => _.Media).WithMany(_ => _.Comments);
            builder.Entity<Comment>().HasOne(_ => _.Gebruiker).WithMany(_ => _.Comments);

            //Playlist
            builder.Entity<Playlist>().HasOne(_ => _.User).WithMany(_ => _.Playlists);

        }
       
    }
}
