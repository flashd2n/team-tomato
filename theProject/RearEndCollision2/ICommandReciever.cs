using System;
using System.Collections.Generic;
using System.Text;

namespace RearEndCollision
{
	public interface ICommandReciever
	{
		void PushCommand(Command cmd);
	}
}
