using System;
using System.Collections.Generic;
using System.Text;

namespace RearEndCollision
{
	public abstract class PlayerInput : ICommandGenerator
	{
        public int PlayerId { get; private set; }

        public PlayerInput(int playerId)
        {
            this.PlayerId = playerId;
        }

		public abstract void ProcessPlayerInput();

		public abstract Command GetCommand();
	}
}
