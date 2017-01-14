using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RearEndCollision
{
	public class GameController
	{
        static void Main()
        {
            List<PlayerInput> playerInputs = new List<PlayerInput>();
            PlayerInputFactory pif = new PlayerInputFactory();
            playerInputs.Add(pif.GetPlayerInput(playerInputs.Count, "local", ConsoleKey.W, ConsoleKey.D, ConsoleKey.S, ConsoleKey.A));

            while(true)
            {
                playerInputs[0].GetCommand();
                Thread.Sleep(1000);
            }

        }
	}
}
