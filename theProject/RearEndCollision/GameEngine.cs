using System;
using System.Collections.Generic;
using System.Text;

namespace RearEndCollision
{
	public class GameEngine : ICommandReciever, IVisualizable
	{
		private ulong currentGameTick;
        private Queue<Command> commandList;
        private IDictionary<int, PlayerState> playerStates; //the key is the player id
        private char[,] map;
        private char[,] mapAndPlayers;
        private int mapRows;
        private int mapCols;
        private Random random;

        public GameEngine()
        {
            currentGameTick = 0;
            commandList = new Queue<Command>();
            playerStates = new Dictionary<int, PlayerState>();
            IsGameRunning = false;
            random = new Random();
        }

        public bool IsGameRunning { set; private get; }

        public ulong StartGame()
        {
            if (mapRows <= 0 || mapCols <= 0)
            {
                throw new InvalidOperationException("Game cannot be started without first loading a map");
            }
            foreach (var player in playerStates)
            {
                int randRow = random.Next(0, mapRows);
                int randCol = random.Next(0, mapRows);
                int fieldsLeft = mapRows * mapCols;
                while (map[randRow, randCol] != ' ')
                {
                    if (fieldsLeft <= 0)
                    {
                        throw new InvalidOperationException(string.Format("No place to place player {0}", player.Key));
                    }
                    randCol++;
                    if (randCol >= mapCols)
                    {
                        randRow = (mapRows + 1) % mapRows;
                        randCol = 0;
                    }
                    fieldsLeft--;
                }
                player.Value.PlayerRow = randRow * 256;
                player.Value.PlayerCol = randCol * 256;
                player.Value.IsDead = false;
                player.Value.PlayerDirection = 'n';
                mapAndPlayers[player.Value.PlayerRow / 256, player.Value.PlayerCol / 256] = (char)('0' + player.Key);
            }
            IsGameRunning = true;
            //for (int i = 0; i < mapRows; i++)
            //{
            //    for (int j = 0; j < mapCols; j++)
            //    {
            //        Console.Write(mapAndPlayers[i, j]);
            //    }
            //    Console.WriteLine();
            //}
            return currentGameTick;
        }

        public ulong AdvanceOneTick()
		{
            while (commandList.Count > 0)
            {
                Command c = commandList.Dequeue();
                PlayerState affectedPlayer = playerStates[c.PlayerId];
                switch(c.PlayerCommand)
                {
                    case CommandType.GoUp:
                        if (affectedPlayer.PlayerDirection != 'u')
                        {
                            if (affectedPlayer.PlayerDirection == 'r')
                            {
                                affectedPlayer.PlayerCol = ((affectedPlayer.PlayerCol + 255) / 256) * 256;
                            }
                            else if (affectedPlayer.PlayerDirection == 'l')
                            {
                                affectedPlayer.PlayerCol = (affectedPlayer.PlayerCol / 256) * 256;
                            }
                            affectedPlayer.PlayerDirection = 'u';
                        }
                        break;
                    case CommandType.GoRight:
                        if (affectedPlayer.PlayerDirection != 'r')
                        {
                            if (affectedPlayer.PlayerDirection == 'd')
                            {
                                affectedPlayer.PlayerRow = ((affectedPlayer.PlayerRow + 255) / 256) * 256;
                            }
                            else if (affectedPlayer.PlayerDirection == 'u')
                            {
                                affectedPlayer.PlayerRow = (affectedPlayer.PlayerRow / 256) * 256;
                            }
                            affectedPlayer.PlayerDirection = 'r';
                        }
                        break;
                    case CommandType.GoDown:
                        if (affectedPlayer.PlayerDirection != 'd')
                        {
                            if (affectedPlayer.PlayerDirection == 'r')
                            {
                                affectedPlayer.PlayerCol = ((affectedPlayer.PlayerCol + 255) / 256) * 256;
                            }
                            else if (affectedPlayer.PlayerDirection == 'l')
                            {
                                affectedPlayer.PlayerCol = (affectedPlayer.PlayerCol / 256) * 256;
                            }
                            affectedPlayer.PlayerDirection = 'd';
                        }
                        break;
                    case CommandType.GoLeft:
                        if (affectedPlayer.PlayerDirection != 'r')
                        {
                            if (affectedPlayer.PlayerDirection == 'd')
                            {
                                affectedPlayer.PlayerRow = ((affectedPlayer.PlayerRow + 255) / 256) * 256;
                            }
                            else if (affectedPlayer.PlayerDirection == 'u')
                            {
                                affectedPlayer.PlayerRow = (affectedPlayer.PlayerRow / 256) * 256;
                            }
                            affectedPlayer.PlayerDirection = 'l';
                        }
                        break;
                }
            }
            return ++currentGameTick;
		}

		public void AddPlayer(int playerId)
		{
            if (IsGameRunning)
            {
                throw new InvalidOperationException("Adding a player while the game is running is supported");
            }
            if (playerStates.ContainsKey(playerId))
            {
                throw new ArgumentException("Player with this id already exists");
            }
            PlayerState newPlayerState = new PlayerState(playerId);
            newPlayerState.IsDead = false;

            playerStates.Add(playerId, newPlayerState);
		}

		public void PushCommand(Command playerCommand)
		{
            commandList.Enqueue(playerCommand);
		}

		public char[,] GetCurrentMap()
		{
            return map;
		}
        public IDictionary<int, PlayerState> GetPlayers()
        {
            IDictionary<int, PlayerState> returnPlayerStates = new Dictionary<int, PlayerState>();
            foreach (var playerState in this.playerStates)
            {
                returnPlayerStates.Add(playerState.Key, playerState.Value.Copy());
            }

            return returnPlayerStates;
        }

        public void LoadMap(MapGenerator mg)
		{
            if (IsGameRunning)
            {
                throw new InvalidOperationException("Loading a map while the game is running is not allowed");
            }
			mapAndPlayers = map =  mg.GenerateMap();
            mapRows = map.GetLength(0);
            mapCols = map.GetLength(1);
		}
    }
}
