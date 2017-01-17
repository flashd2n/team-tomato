using System;
using System.Collections.Generic;
using System.Text;

namespace RearEndCollision
{
    using System.Runtime.CompilerServices;

    public class MapRandomGenerator
	{
        // TODO: The randomnes factor can be implemented by simple Prim algo using: random number of MST node count. Inh from *MapGenerator.


        // This a pseudo Random Generator - hacked
        public MapRandomGenerator()
	    {
            //in method ...
            Random rnd = new Random();
	        int lastMapDeveloped = 3;
	        string mapPath = $"..\\..\\DemoMaps\\testmap.{rnd.Next(0, lastMapDeveloped + 1)}.txt";

            this.CurrentRandomMap = mapPath;
	    }

	    public string CurrentRandomMap { get; }
	}
}
