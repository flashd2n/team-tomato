using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RearEndCollision
{
    public class NoSuchPlayerTypeException : ArgumentException
    {
        public string PlayerType { get; private set;}
        public NoSuchPlayerTypeException(string message, string playerType): base(message)
        {
            this.PlayerType = playerType;
        }
    }
}
