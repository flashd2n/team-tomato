using System;
using System.Collections.Generic;
using System.Text;

namespace RearEndCollision
{
	public class PlayerInputFactory
	{
		public PlayerInput GetPlayerInput(int playerId, string inputPlayerType, params Object[] additionalArgs)
		{
			switch (inputPlayerType)
            {
                case "local":
                    if (additionalArgs.Length < 4)
                    {
                        throw new ArgumentException("Local player requires four ConsoleKey arguments for up, right, down and left respectively");
                    }
                    ConsoleKey[] directions = new ConsoleKey[4];
                    for (int i = 0; i < 4; i++)
                    {
                        if (!(additionalArgs[i] is ConsoleKey))
                        {
                            throw new ArgumentException(string.Format("Argument {0} is not of ConsoleKey type", i + 1));
                        }
                        directions[i] = (ConsoleKey)additionalArgs[i];
                    }
                    return new LocalPlayerInput(playerId, directions[0], directions[1], directions[2], directions[3]);
                case "network":
                    return new NetworkPlayerInput(playerId);
                case "ai":
                    return new AiPlayerInput(playerId);
                default:
                    throw new NoSuchPlayerTypeException("Invalid player type " + inputPlayerType, inputPlayerType);
            }
		}
	}
}
