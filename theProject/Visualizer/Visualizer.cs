using System;
using System.Collections.Generic;
using System.Text;

namespace RearEndCollision
{
	public abstract class Visualizer
	{
		IVisualizable mapWithPlayers;

		public abstract void SetMapAndPlayerSource(IVisualizable mapAndPlayers);

		public abstract void VisualizeNow();
	}
}
