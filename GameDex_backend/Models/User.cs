using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameDex_backend.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime RegisterDate { get; set; }
        public int[] FavGames { get; set; }
    }
}
