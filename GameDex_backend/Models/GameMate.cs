using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameDex_backend.Models
{
    public class GameMate
    {
        public int Id { get; set; }
        public virtual User FriendFrom { get; set; }
        public virtual User FriendTo { get; set; }
        public bool isConfirmed { get; set; }
    }
}
