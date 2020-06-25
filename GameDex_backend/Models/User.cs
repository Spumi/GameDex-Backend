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
            RegisterDate = DateTime.Now;
            FavGames = new List<Favourite>();
            FavPublishers = new List<FavouritePublisher>();
            FavDeveloper = new List<FavouriteDeveloper>();
            GameMates = new List<GameMate>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime RegisterDate { get; set; }
        public List<Favourite> FavGames { get; set; }
        public List<FavouritePublisher> FavPublishers { get; set; }
        public List<FavouriteDeveloper> FavDeveloper { get; set; }
        public string Auth_token { get; set; }
        public List<GameMate> GameMates { get; set; }

        internal string GenerateAuthToken()
        {
            string toHash = Username + Password + Stopwatch.GetTimestamp().ToString();
            return toHash.GetHashCode().ToString();
        }
    }
}
