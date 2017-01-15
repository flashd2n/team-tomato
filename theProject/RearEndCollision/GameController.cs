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
            playerInputs.Add(pif.GetPlayerInput(playerInputs.Count, "local", ConsoleKey.UpArrow, ConsoleKey.RightArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow));

            MapGeneratorFactory mgf = new MapGeneratorFactory();
            MapGenerator mg = mgf.GetMapGenerator("empty", 25, 100);

            VisualizerFactory vf = new VisualizerFactory();

            GameEngine engine = new GameEngine();

            Visualizer vis = vf.GetVisualizer("console", engine);
            for (int i = 0; i < playerInputs.Count; i++)
            {
                engine.AddPlayer(i);
            }
            engine.LoadMap(mg);
            engine.StartGame();

            vis.VisualizeNow();

            while(true)
            {
                playerInputs[0].GetCommand();
                Thread.Sleep(1000);
            }

        }
	}
}
