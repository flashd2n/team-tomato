using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RearEndCollision
{
    public class PlayerState: IComparable<PlayerState>
    {
        public const int POSITION_DIVIDER = 4096;

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

        public int CompareTo(PlayerState other)
        {
            int cmp;
            cmp = this.PlayerId.CompareTo(other.PlayerId);
            if (cmp != 0) return cmp;
            cmp = this.PlayerRow.CompareTo(other.PlayerRow);
            if (cmp != 0) return cmp;
            cmp = this.PlayerCol.CompareTo(other.PlayerCol);
            if (cmp != 0) return cmp;
            cmp = this.PlayerDirection.CompareTo(other.PlayerDirection);
            if (cmp != 0) return cmp;
            cmp = this.IsDead.CompareTo(other.IsDead);
            if (cmp != 0) return cmp;
            return 0;
        }
    }
}
