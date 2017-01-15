using System;
using System.Collections.Generic;
using System.Text;

namespace RearEndCollision
{
	public abstract class Visualizer
	{
		protected IVisualizable MapWithPlayers;

        public Visualizer(IVisualizable mapWithPlayers)
        {
            this.MapWithPlayers = mapWithPlayers;
        }

		public abstract void VisualizeNow();
	}
}
