using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RearEndCollision
{
    public class PlayerState
    {
        public int PlayerId;
        public long PlayerRow; //one field equals to 512 steps
        public long PlayerCol; //one field equals to 512 steps
        public char PlayerDirection; //u, r, d, l, n n means no direction. Only when starting.
        public bool IsDead;

        public PlayerState(int playerId)
        {
            this.PlayerId = playerId;
        }
        
        public PlayerState Copy()
        {
            return this.MemberwiseClone() as PlayerState;
        }
    }
}
