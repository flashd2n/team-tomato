using System;
using System.Collections.Generic;
using System.Text;

namespace RearEndCollision
{
	public class GameEngine : ICommandReciever, IVisualizable
	{
		ulong currentGameTick;
		List<Commad> commandList;

		public ulong AdvanceOneTick()
		{
			throw new NotImplementedException();
		}

		public void NewMethod(char[,] map)
		{
			throw new NotImplementedException();
		}

		public void AddPlayers(int playerCount)
		{
			throw new NotImplementedException();
		}

		public void PushCommand(Command playerCommand)
		{
			throw new NotImplementedException();
		}

		public char[,] GetCurrentMapAndPlayers()
		{
			throw new NotImplementedException();
		}

		public void LoadMap(MapGenerator mg)
		{
			throw new NotImplementedException();
		}
	}
}
