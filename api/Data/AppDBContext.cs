using System;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<AuthProvider> AuthProviders { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<UserFavoriteCity> UserFavoriteCities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // M:M

            modelBuilder.Entity<UserFavoriteCity>(e => e.HasKey(uf => new { uf.UserId, uf.CityId }));

            modelBuilder.Entity<UserFavoriteCity>()
                .HasOne(uf => uf.User)
                .WithMany(u => u.FavoriteCities)
                .HasForeignKey(uf => uf.UserId);

            modelBuilder.Entity<UserFavoriteCity>()
                .HasOne(uf => uf.City)
                .WithMany(c => c.FavoriteUsers)
                .HasForeignKey(uf => uf.CityId);

            // 1:M

            modelBuilder.Entity<UserLogin>()
                .HasOne(l => l.User)
                .WithMany(p => p.UserLogins)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserLogin>()
                .HasOne(l => l.Provider)
                .WithMany(p => p.UserLogins)
                .HasForeignKey(l => l.ProviderId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<UserLogin>()
                .HasIndex(l => new { l.UserId, l.ProviderId, l.SubId })
                .IsUnique()
                .HasFilter("[SubId] IS NOT NULL");

            // Seed providers
            modelBuilder.Entity<AuthProvider>().HasData(
                new AuthProvider { Id = 1, Name = "google" },
                new AuthProvider { Id = 2, Name = "facebook" }
            );

            modelBuilder.Entity<City>().HasData(
                new City { Id = 1248991, Name = "Colombo", Country = "LK" },
                new City { Id = 1850147, Name = "Tokyo", Country = "JP" },
                new City { Id = 2644210, Name = "Liverpool", Country = "GB" },
                new City { Id = 2988507, Name = "Paris", Country = "FR" },
                new City { Id = 2147714, Name = "Sydney", Country = "AU" },
                new City { Id = 4930956, Name = "Boston", Country = "US" },
                new City { Id = 1796236, Name = "Shanghai", Country = "CN" },
                new City { Id = 3143244, Name = "Oslo", Country = "NO" },
                new City { Id = 5128581, Name = "New York", Country = "US" },
                new City { Id = 2643743, Name = "London", Country = "GB" },
                new City { Id = 2950159, Name = "Berlin", Country = "DE" },
                new City { Id = 3169070, Name = "Rome", Country = "IT" },
                new City { Id = 524901, Name = "Moscow", Country = "RU" },
                new City { Id = 1816670, Name = "Beijing", Country = "CN" },
                new City { Id = 5368361, Name = "Los Angeles", Country = "US" },
                new City { Id = 3128760, Name = "Barcelona", Country = "ES" },
                new City { Id = 1880252, Name = "Singapore", Country = "SG" },
                new City { Id = 1835848, Name = "Seoul", Country = "KR" }
            );



        }

    }
}