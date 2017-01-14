using System;
using System.Collections.Generic;
using System.Text;

namespace RearEndCollision
{
	public class LocalPlayerInput : PlayerInput
	{
        private ConsoleKey keyUp;
        private ConsoleKey keyRight;
        private ConsoleKey keyDown;
        private ConsoleKey keyLeft;

        public LocalPlayerInput(int playerId, ConsoleKey keyUp, ConsoleKey keyRight, ConsoleKey keyDown, ConsoleKey keyLeft): base(playerId)
        {
            this.keyUp = keyUp;
            this.keyRight = keyRight;
            this.keyDown = keyDown;
            this.keyLeft = keyLeft;
        }

		public override void ProcessPlayerInput()
		{

            throw new NotImplementedException();
		}

		public override Command GetCommand()
		{
            Command c;

            c.GameTick = 0; //TODO: set correct game tick
            c.PlayerId = this.PlayerId;
            c.PlayerCommand = CommandType.Idle;

            KeyboardInputSource kis = KeyboardInputSource.GetKeyboardInputSource();
            if (kis.HasKeyBeenPressed(keyUp))
            {
                Console.WriteLine("UP");
                c.PlayerCommand = CommandType.GoUp;
            }
            if (kis.HasKeyBeenPressed(keyRight))
            {
                Console.WriteLine("RIGHT");
                c.PlayerCommand = CommandType.GoRight;
            }
            if (kis.HasKeyBeenPressed(keyDown))
            {
                Console.WriteLine("DOWN");
                c.PlayerCommand = CommandType.GoDown;
            }
            if (kis.HasKeyBeenPressed(keyLeft))
            {
                Console.WriteLine("LEFT");
                c.PlayerCommand = CommandType.GoLeft;
            }


            return c;
		}
	}
}
