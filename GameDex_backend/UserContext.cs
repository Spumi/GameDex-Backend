using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameDex_backend.Models;


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
    }
}

