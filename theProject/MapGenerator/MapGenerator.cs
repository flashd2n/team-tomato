using System;
using System.Collections.Generic;
using System.Text;

namespace RearEndCollision
{
	public abstract class MapGenerator
	{
		public abstract char[,] GenerateMap();
	}
}
