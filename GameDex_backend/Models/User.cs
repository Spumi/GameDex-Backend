using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameDex_backend.Models
{
    public class User
    {
        public User()
        {
            RegisterDate = DateTime.Now;
            FavGames = new List<Favourite>();
            FavPublishers = new List<FavouritePublisher>();
            FavDeveloper = new List<FavouriteDeveloper>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime RegisterDate { get; set; }
        public List<Favourite> FavGames { get; set; }
        public List<FavouritePublisher> FavPublishers { get; set; }
        public List<FavouriteDeveloper> FavDeveloper { get; set; }
        public string Auth_token { get; set; }

        internal string GenerateAuthToken()
        {
            string toHash = Username + Password + Stopwatch.GetTimestamp().ToString();
            return toHash.GetHashCode().ToString();
        }
    }
}