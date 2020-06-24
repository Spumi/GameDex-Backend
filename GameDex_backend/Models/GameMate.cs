using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameDex_backend.Models
{
    public class GameMate
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public bool IsAccepted { get; set; }
    }
}
