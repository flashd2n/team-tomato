using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RearEndCollision
{
    public class MapGeneratorFactory
    {
        private static string currentMapChosen;


        public MapGenerator GetMapGenerator(string type, params Object[] additionalParams)
        {
            var rm = new MapRandomGenerator();
            additionalParams[0] = rm.CurrentRandomMap;
            switch (type)
            {
                case "empty":
                    if (!(additionalParams[0] is int))
                    {
                        throw new ArgumentException(string.Format("Additional parameters must be of type int for map generator type {0}", type));
                    }
                    if (!(additionalParams[1] is int))
                    {
                        throw new ArgumentException(string.Format("Additional parameters must be of type int for map generator type {0}", type));
                    }
                    int rows = (int)additionalParams[0];
                    int cols = (int)additionalParams[1];
                    if (rows < 0 || cols < 0 || rows > MapGenerator.MAX_ROWS || cols > MapGenerator.MAX_COLS)
                    {
                        throw new ArgumentException(string.Format("Rows must be between {0} and {1} and columns must be between {2} and {3}", 0, MapGenerator.MAX_ROWS, 0, MapGenerator.MAX_COLS));
                    }
                    return new MapGenerateEmpty(rows, cols);
                case "file":
                    if (!(additionalParams[0] is string))
                    {
                        throw new ArgumentException(string.Format("Argument must contain filename"));
                    }
                    return new MapGeneratorFromFile((string) additionalParams[0]);
                default:
                    throw new ArgumentException(string.Format("Unrecognized map generator type {0}", type));
            }
        }
    }
}
