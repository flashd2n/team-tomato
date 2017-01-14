using System;
using System.Collections.Generic;
using System.Text;

namespace RearEndCollision
{
	public class NetworkPlayerInput : PlayerInput
	{
        public NetworkPlayerInput(int playerId) : base(playerId)
        {
        }

        public override Command GetCommand()
        {
            throw new NotImplementedException();
        }

        public override void ProcessPlayerInput()
		{
			throw new NotImplementedException();
		}
	}
}
