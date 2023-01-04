using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudPlayers
{
    public class Player
    {
        public string nickname { get; set; }
        public string function { get; set; }
        public int id { get; set; }
        
        public Player() { }

        public Player(string nickname, string function, int id)
        {
            this.nickname = nickname;
            this.function = function;
            this.id = id;
        }

        public Player CopiarPlayer()
        {
            return (Player)MemberwiseClone();
        }
    }
}
