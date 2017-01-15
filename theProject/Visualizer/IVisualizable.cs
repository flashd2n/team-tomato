using System;
using System.Collections.Generic;
using System.Text;

namespace RearEndCollision
{
	public interface IVisualizable
	{
		char[,] GetCurrentMap();
        IDictionary<int, PlayerState> GetPlayers();
	}
}
