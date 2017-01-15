using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RearEndCollision
{
	public class GameController
	{
        const int GAME_TICK_LENGTH_MS = 10;
        static void Main()
        {
            //Player Inputs. Where is the player?
            List<PlayerInput> playerInputs = new List<PlayerInput>();
            PlayerInputFactory pif = new PlayerInputFactory();
            playerInputs.Add(pif.GetPlayerInput(playerInputs.Count, "local", ConsoleKey.W, ConsoleKey.D, ConsoleKey.S, ConsoleKey.A));
            playerInputs.Add(pif.GetPlayerInput(playerInputs.Count, "local", ConsoleKey.UpArrow, ConsoleKey.RightArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow));
            playerInputs.Add(pif.GetPlayerInput(playerInputs.Count, "local", ConsoleKey.I, ConsoleKey.L, ConsoleKey.K, ConsoleKey.J));

            //Generating map
            MapGeneratorFactory mgf = new MapGeneratorFactory();
            //MapGenerator mg = mgf.GetMapGenerator("empty", 25, 100);
            MapGenerator mg = mgf.GetMapGenerator("file", "..\\..\\DemoMaps\\testmap.txt");

            VisualizerFactory vf = new VisualizerFactory();

            GameEngine engine = new GameEngine();

            CommandDispatcher cd = new CommandDispatcher();

            foreach(PlayerInput pi in playerInputs)
            {
                cd.AddCommandGenerator(pi);
            }

            cd.AddCommandReciever(engine);

            Visualizer vis = vf.GetVisualizer("console", engine);
            for (int i = 0; i < playerInputs.Count; i++)
            {
                engine.AddPlayer(i);
            }
            engine.LoadMap(mg);
            engine.StartGame();

            DateTime lastAdvanceTime = DateTime.UtcNow;
            while(true)
            {
                TimeSpan timeDiff = DateTime.UtcNow - lastAdvanceTime;
                int millisecondDiff = (int)timeDiff.TotalMilliseconds;
                if (millisecondDiff >= GAME_TICK_LENGTH_MS)
                {
                    cd.DispatchCommands();
                    engine.AdvanceOneTick();
                    vis.VisualizeNow();
                    lastAdvanceTime = lastAdvanceTime.AddMilliseconds(GAME_TICK_LENGTH_MS);
                    if (!engine.IsGameRunning)
                    {
                        break;
                    }
                }
                else
                {
                    Thread.Sleep(GAME_TICK_LENGTH_MS - millisecondDiff);
                }
            }
            if (engine.IsGameDraw)
            {
                vis.DisplayMessage("The game ended in a draw!");
            }
            else
            {
                vis.DisplayMessage(string.Format("Player {0} won the game!", engine.WinningPlayerId));
            }
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
            } while (key.Key != ConsoleKey.Escape);
        }
	}
}
