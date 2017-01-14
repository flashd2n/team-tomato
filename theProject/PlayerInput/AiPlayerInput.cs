using System;
using System.Collections.Generic;
using System.Text;

namespace RearEndCollision
{
	public class AiPlayerInput : PlayerInput
	{
        public AiPlayerInput(int playerId) : base(playerId)
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
