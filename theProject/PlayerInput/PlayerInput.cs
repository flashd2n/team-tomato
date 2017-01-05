using System;
using System.Collections.Generic;
using System.Text;

namespace RearEndCollision
{
	public abstract class PlayerInput : ICommandGenerator
	{
		public abstract void ProcessPlayerInput();

		public abstract Command GetCommand();
	}
}
