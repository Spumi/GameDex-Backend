using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameDex_backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime RegisterDate { get; set; }
        //public List<int> FavGames { get; set; }
        public string Auth_token { get; set; }
    }
}
