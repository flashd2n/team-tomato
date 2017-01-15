using System;
using System.Collections.Generic;
using System.Text;

namespace RearEndCollision
{
	public abstract class MapGenerator
	{
        public const int MAX_ROWS = 60;
        public const int MAX_COLS = 150;
		public abstract char[,] GenerateMap();
	}
}
