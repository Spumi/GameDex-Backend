using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameDex_backend.Models
{
    public class Favourite
    {
        public int Id { get; set; }
        public int FavId { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public string Auth_token { get;  set; }
    }
}
