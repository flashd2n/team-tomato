using System;
using System.Collections.Generic;
using System.Text;

namespace RearEndCollision
{
	public class VisualizerFactory
	{
		public Visualizer GetVisualizer(string visualizerType, IVisualizable mapWithPlayers)
		{
			switch (visualizerType)
            {
                case "console":
                    return new ConsoleVisualizer(mapWithPlayers);
                default:
                    throw new ArgumentException(string.Format("Unknown visualizer type {0}", visualizerType));
            }
		}
	}
}
