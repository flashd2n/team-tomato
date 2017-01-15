using System;
using System.Collections.Generic;
using System.Text;

namespace RearEndCollision
{
	public class ConsoleVisualizer : Visualizer
	{
        private IDictionary<int, PlayerState> prevPlayerStates;
        private bool isMapDrawn;

        public ConsoleVisualizer(IVisualizable mapWithPlayers): base(mapWithPlayers)
        {
            isMapDrawn = false;
            Console.CursorVisible = false;
        }

		public override void VisualizeNow()
		{
			if (!isMapDrawn)
            {
                char[,] map = this.MapWithPlayers.GetCurrentMap();
                int rows = map.GetLength(0);
                int cols = map.GetLength(1);

                //draw outline
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.BackgroundColor = ConsoleColor.Blue;
                for (int row = 0; row <= rows + 1; row++)
                {
                    Console.SetCursorPosition(0, row);
                    Console.Write('#');
                    Console.SetCursorPosition(cols + 1, row);
                    Console.Write('#');
                }
                for (int col = 0; col <= cols + 1; col++)
                {

                        Console.SetCursorPosition(col, 0);
                        Console.Write('#');
                        Console.SetCursorPosition(col, rows + 1);
                        Console.Write('#');
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Red;
                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        if (map[row, col] == '#')
                        {
                            Console.SetCursorPosition(col + 1, row + 1);
                            Console.Write('#');
                        }
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                isMapDrawn = true;
            }

            IDictionary<int, PlayerState> currentPlayerStates = this.MapWithPlayers.GetPlayers();
            foreach (var player in currentPlayerStates)
            {
                bool isPlayerDiff = true;
                if (prevPlayerStates != null && prevPlayerStates.ContainsKey(player.Key))
                {                    
                    PlayerState prevPlayerState = prevPlayerStates[player.Key];
                    isPlayerDiff = player.Value.CompareTo(prevPlayerState) != 0;
                    if (isPlayerDiff)
                    {
                        Console.SetCursorPosition((int)(prevPlayerState.PlayerRow / 256) + 1, (int)(prevPlayerState.PlayerCol / 256) + 1);
                        Console.Write(' ');
                    }
                }

                if (!player.Value.IsDead && isPlayerDiff)
                {
                    Console.SetCursorPosition((int)(player.Value.PlayerRow / 256) + 1, (int)(player.Value.PlayerCol / 256) + 1);
                    char playerChar = (char)('0' + player.Value.PlayerId);
                    switch (player.Value.PlayerDirection)
                    {
                        case 'u':
                            playerChar = '^';
                            break;
                        case 'r':
                            playerChar = '>';
                            break;
                        case 'd':
                            playerChar = 'v';
                            break;
                        case 'l':
                            playerChar = '<';
                            break;
                    }
                    Console.Write(playerChar);
                }
            }
            prevPlayerStates = currentPlayerStates;
        }
	}
}
