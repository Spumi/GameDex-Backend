using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GameDex_backend.Models
{
    public class User
    {
        public User() {
            //this.FavGames = new List<string>();

        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime RegisterDate { get; set; }
        //public List<string> FavGames { get; set; }
        public string Auth_token { get; set; }

        internal string GenerateAuthToken()
        {
            string toHash = Username + Password + Stopwatch.GetTimestamp().ToString();
            return toHash.GetHashCode().ToString();
        }
    }
}
