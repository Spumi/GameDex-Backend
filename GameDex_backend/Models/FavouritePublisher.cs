using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GameDex_backend.Models
{
    public class FavouritePublisher
    {
        public int Id { get; set; }
        public int FavId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [JsonIgnore]
        public string Auth_token { get; set; }
    }
}