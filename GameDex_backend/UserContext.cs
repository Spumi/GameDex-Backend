using GameDex_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace GameDex_backend
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<GameDex_backend.Models.Favourite> Favourite { get; set; }

        public DbSet<GameDex_backend.Models.FavouritePublisher> FavouritePublisher { get; set; }

        public DbSet<GameDex_backend.Models.FavouriteDeveloper> FavouriteDeveloper { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlite("Data Source=blogging.db");
    }
}