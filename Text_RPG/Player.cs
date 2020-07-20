using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp
{
    public enum PlayerType
    {
        None = 0,
        Knight = 1,
        Archer = 2,
        Mage = 3
    }
    class Player : Creature
    {
        protected PlayerType type = PlayerType.None;
       
        
        protected Player(PlayerType type) : base(CreatureType.Player)
        {
            this.type = type;
        }

     
    }

    class Knight : Player
    {
        public Knight():base(PlayerType.Knight)
        {
            SetInfo(100, 10);
        }
    }

    class Archer : Player
    {
        public Archer():base(PlayerType.Archer)
        {
            SetInfo(75, 12);
        }
    }

    class Mage : Player
    {
        public Mage():base(PlayerType.Mage)
        {
            SetInfo(75, 15);
        }
    }
}
