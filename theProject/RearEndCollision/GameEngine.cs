using System;
using System.Collections.Generic;
using System.Text;

namespace RearEndCollision
{
    public class GameEngine : ICommandReciever, IVisualizable
    {
        const int NORMAL_SPEED_PER_TICK = 10;
        private ulong currentGameTick;
        private Queue<Command> commandList;
        private IDictionary<int, PlayerState> playerStates; //the key is the player id
        private char[,] map;
        private int mapRows;
        private int mapCols;
        private Random random;
        private int playersAlive;

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
                int randCol = random.Next(0, mapCols);

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
                player.Value.PlayerRow = randRow * PlayerState.POSITION_DIVIDER;
                player.Value.PlayerCol = randCol * PlayerState.POSITION_DIVIDER;
                player.Value.IsDead = false;
                player.Value.PlayerDirection = 'n';
            }

            playersAlive = playerStates.Count;

            IsGameRunning = true;
           
            return currentGameTick;
        }

        public ulong AdvanceOneTick()
        {
            while (commandList.Count > 0)
            {
                Command c = commandList.Dequeue();
                PlayerState affectedPlayer = playerStates[c.PlayerId];
                if (!affectedPlayer.IsDead)
                {
                    switch (c.PlayerCommand)
                    {
                        case CommandType.GoUp:
                            if (affectedPlayer.PlayerDirection != 'u' && affectedPlayer.PlayerDirection != 'd')
                            {
                                if (affectedPlayer.PlayerDirection == 'r')
                                {
                                    affectedPlayer.PlayerCol = ((affectedPlayer.PlayerCol + PlayerState.POSITION_DIVIDER - 1) / PlayerState.POSITION_DIVIDER) * PlayerState.POSITION_DIVIDER;
                                }
                                else if (affectedPlayer.PlayerDirection == 'l')
                                {
                                    affectedPlayer.PlayerCol = (affectedPlayer.PlayerCol / PlayerState.POSITION_DIVIDER) * PlayerState.POSITION_DIVIDER;
                                }
                                affectedPlayer.PlayerDirection = 'u';
                            }
                            break;
                        case CommandType.GoRight:
                            if (affectedPlayer.PlayerDirection != 'r' && affectedPlayer.PlayerDirection != 'l')
                            {
                                if (affectedPlayer.PlayerDirection == 'd')
                                {
                                    affectedPlayer.PlayerRow = ((affectedPlayer.PlayerRow + PlayerState.POSITION_DIVIDER - 1) / PlayerState.POSITION_DIVIDER) * PlayerState.POSITION_DIVIDER;
                                }
                                else if (affectedPlayer.PlayerDirection == 'u')
                                {
                                    affectedPlayer.PlayerRow = (affectedPlayer.PlayerRow / PlayerState.POSITION_DIVIDER) * PlayerState.POSITION_DIVIDER;
                                }
                                affectedPlayer.PlayerDirection = 'r';
                            }
                            break;
                        case CommandType.GoDown:
                            if (affectedPlayer.PlayerDirection != 'u' && affectedPlayer.PlayerDirection != 'd')
                            {
                                if (affectedPlayer.PlayerDirection == 'r')
                                {
                                    affectedPlayer.PlayerCol = ((affectedPlayer.PlayerCol + PlayerState.POSITION_DIVIDER - 1) / PlayerState.POSITION_DIVIDER) * PlayerState.POSITION_DIVIDER;
                                }
                                else if (affectedPlayer.PlayerDirection == 'l')
                                {
                                    affectedPlayer.PlayerCol = (affectedPlayer.PlayerCol / PlayerState.POSITION_DIVIDER) * PlayerState.POSITION_DIVIDER;
                                }
                                affectedPlayer.PlayerDirection = 'd';
                            }
                            break;
                        case CommandType.GoLeft:
                            if (affectedPlayer.PlayerDirection != 'r' && affectedPlayer.PlayerDirection != 'l')
                            {
                                if (affectedPlayer.PlayerDirection == 'd')
                                {
                                    affectedPlayer.PlayerRow = ((affectedPlayer.PlayerRow + PlayerState.POSITION_DIVIDER - 1) / PlayerState.POSITION_DIVIDER) * PlayerState.POSITION_DIVIDER;
                                }
                                else if (affectedPlayer.PlayerDirection == 'u')
                                {
                                    affectedPlayer.PlayerRow = (affectedPlayer.PlayerRow / PlayerState.POSITION_DIVIDER) * PlayerState.POSITION_DIVIDER;
                                }
                                affectedPlayer.PlayerDirection = 'l';
                            }
                            break;
                    }
                }
            }

            foreach (var ps in playerStates)
            {
                if (ps.Value.IsDead) continue;

                int speed = calculateSpeed(ps.Value);
                switch (ps.Value.PlayerDirection)
                {
                    case 'u':
                        ps.Value.PlayerRow -= speed;
                        break;
                    case 'r':
                        ps.Value.PlayerCol += speed;
                        break;
                    case 'd':
                        ps.Value.PlayerRow += speed;
                        break;
                    case 'l':
                        ps.Value.PlayerCol -= speed;
                        break;
                }

                int playerRowGrid = (int)(ps.Value.PlayerRow / PlayerState.POSITION_DIVIDER);
                int playerColGrid = (int)(ps.Value.PlayerCol / PlayerState.POSITION_DIVIDER);

                if (map[playerRowGrid, playerColGrid] != ' '
                    || ps.Value.PlayerRow < 0 || ps.Value.PlayerCol < 0 
                    || playerRowGrid > this.mapRows || playerColGrid > this.mapCols)
                {
                    ps.Value.IsDead = true;
                    this.playersAlive--;
                }
            }

            processPlayerCollisions();

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
            map = mg.GenerateMap();
            mapRows = map.GetLength(0);
            mapCols = map.GetLength(1);
        }

        private int calculateSpeed(PlayerState player)
        {
            if (player.IsDead) return 0;

            int speed = NORMAL_SPEED_PER_TICK;

            foreach (var playerState in this.playerStates)
            {
                if (playerState.Value == player || playerState.Value.IsDead)
                {
                    continue;
                }

                if (player.PlayerCol == playerState.Value.PlayerCol)
                {
                    if (player.PlayerDirection == 'u' && playerState.Value.PlayerDirection == 'u'
                        && player.PlayerCol == playerState.Value.PlayerCol && player.PlayerRow > playerState.Value.PlayerRow)
                    {
                        int row = (int)(player.PlayerRow / PlayerState.POSITION_DIVIDER);
                        int col = (int)(player.PlayerCol / PlayerState.POSITION_DIVIDER);

                        int otherRow = (int)(playerState.Value.PlayerRow / PlayerState.POSITION_DIVIDER);
                        int otherCol = (int)(playerState.Value.PlayerCol / PlayerState.POSITION_DIVIDER);

                        for (; row > otherRow; row--)
                        {
                            if (map[row, col] != ' ')
                            {
                                break;
                            }
                        }
                        if (row == otherRow)
                        {
                            int newSpeed = (int)(NORMAL_SPEED_PER_TICK + PlayerState.POSITION_DIVIDER * 50 / (player.PlayerRow - playerState.Value.PlayerRow));
                            if (newSpeed > speed)
                            {
                                speed = newSpeed;
                            }
                        }
                    }
                    else if (player.PlayerDirection == 'd' && playerState.Value.PlayerDirection == 'd'
                        && player.PlayerCol == playerState.Value.PlayerCol && player.PlayerRow < playerState.Value.PlayerRow)
                    {
                        int row = (int)(player.PlayerRow / PlayerState.POSITION_DIVIDER);
                        int col = (int)(player.PlayerCol / PlayerState.POSITION_DIVIDER);

                        int otherRow = (int)(playerState.Value.PlayerRow / PlayerState.POSITION_DIVIDER);
                        int otherCol = (int)(playerState.Value.PlayerCol / PlayerState.POSITION_DIVIDER);

                        for (; row < otherRow; row++)
                        {
                            if (map[row, col] != ' ')
                            {
                                break;
                            }
                        }
                        if (row == otherRow)
                        {
                            int newSpeed = (int)(NORMAL_SPEED_PER_TICK + PlayerState.POSITION_DIVIDER * 50 / (playerState.Value.PlayerRow - player.PlayerRow));
                            if (newSpeed > speed)
                            {
                                speed = newSpeed;
                            }
                        }
                    }
                }
                else if (player.PlayerRow == playerState.Value.PlayerRow)
                {
                    if (player.PlayerDirection == 'l' && playerState.Value.PlayerDirection == 'l'
                        && player.PlayerRow == playerState.Value.PlayerRow && player.PlayerCol > playerState.Value.PlayerCol)
                    {
                        int row = (int)(player.PlayerRow / PlayerState.POSITION_DIVIDER);
                        int col = (int)(player.PlayerCol / PlayerState.POSITION_DIVIDER);

                        int otherRow = (int)(playerState.Value.PlayerRow / PlayerState.POSITION_DIVIDER);
                        int otherCol = (int)(playerState.Value.PlayerCol / PlayerState.POSITION_DIVIDER);

                        for (; col > otherCol; col--)
                        {
                            if (map[row, col] != ' ')
                            {
                                break;
                            }
                        }
                        if (col == otherCol)
                        {
                            int newSpeed = (int)(NORMAL_SPEED_PER_TICK + PlayerState.POSITION_DIVIDER * 50 / (player.PlayerCol - playerState.Value.PlayerCol));
                            if (newSpeed > speed)
                            {
                                speed = newSpeed;
                            }
                        }
                    }
                    else if (player.PlayerDirection == 'r' && playerState.Value.PlayerDirection == 'r'
                        && player.PlayerRow == playerState.Value.PlayerRow && player.PlayerCol < playerState.Value.PlayerCol)
                    {
                        int row = (int)(player.PlayerRow / PlayerState.POSITION_DIVIDER);
                        int col = (int)(player.PlayerCol / PlayerState.POSITION_DIVIDER);

                        int otherRow = (int)(playerState.Value.PlayerRow / PlayerState.POSITION_DIVIDER);
                        int otherCol = (int)(playerState.Value.PlayerCol / PlayerState.POSITION_DIVIDER);

                        for (; col < otherCol; col++)
                        {
                            if (map[row, col] != ' ')
                            {
                                break;
                            }
                        }
                        if (col == otherCol)
                        {
                            int newSpeed = (int)(NORMAL_SPEED_PER_TICK + PlayerState.POSITION_DIVIDER * 50 / (playerState.Value.PlayerCol - player.PlayerCol));
                            if (newSpeed > speed)
                            {
                                speed = newSpeed;
                            }
                        }
                    }
                }
            }

            if (speed > PlayerState.POSITION_DIVIDER)
            {
                speed = PlayerState.POSITION_DIVIDER - 1;
            }

            return speed;
        }

        private void processPlayerCollisions()
        {
            foreach (var playerX in this.playerStates)
            {
                if (playerX.Value.IsDead)
                {
                    continue;
                }
                foreach (var playerY in this.playerStates)
                {
                    if (playerX.Value == playerY.Value || playerY.Value.IsDead)
                    {
                        continue;
                    }

                    int row = (int)(playerX.Value.PlayerRow / PlayerState.POSITION_DIVIDER);
                    int col = (int)(playerX.Value.PlayerCol / PlayerState.POSITION_DIVIDER);

                    int otherRow = (int)(playerY.Value.PlayerRow / PlayerState.POSITION_DIVIDER);
                    int otherCol = (int)(playerY.Value.PlayerCol / PlayerState.POSITION_DIVIDER);

                    if (row == otherRow && col == otherCol)
                    {
                        if (playerX.Value.PlayerDirection == 'n')
                        {
                            playerX.Value.IsDead = true;
                            this.playersAlive--;
                        }
                        if (playerY.Value.PlayerDirection == 'n')
                        {
                            playerY.Value.IsDead = true;
                            this.playersAlive--;
                        }
                        if (playerX.Value.IsDead || playerY.Value.IsDead)
                        {
                            continue;
                        }

                        if (playerX.Value.PlayerDirection != playerY.Value.PlayerDirection)
                        {
                            playerX.Value.IsDead = true;
                            playerY.Value.IsDead = true;
                            this.playersAlive -= 2;
                            continue;
                        }
                        
                        //they move in the same direction
                        switch (playerX.Value.PlayerDirection)
                        {
                            case 'u':
                                if (playerX.Value.PlayerRow < playerY.Value.PlayerRow)
                                {
                                    playerX.Value.IsDead = true;
                                }
                                else
                                {
                                    playerY.Value.IsDead = true;
                                }
                                break;
                            case 'd':
                                if (playerX.Value.PlayerRow > playerY.Value.PlayerRow)
                                {
                                    playerX.Value.IsDead = true;
                                }
                                else
                                {
                                    playerY.Value.IsDead = true;
                                }
                                break;
                            case 'l':
                                if (playerX.Value.PlayerCol < playerY.Value.PlayerCol)
                                {
                                    playerX.Value.IsDead = true;
                                }
                                else
                                {
                                    playerY.Value.IsDead = true;
                                }
                                break;
                            case 'r':
                                if (playerX.Value.PlayerRow > playerY.Value.PlayerRow)
                                {
                                    playerX.Value.IsDead = true;
                                }
                                else
                                {
                                    playerY.Value.IsDead = true;
                                }
                                break;

                        }
                        this.playersAlive--;
                    }
                }
            }
        }
    }
}
