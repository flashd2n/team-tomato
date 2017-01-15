using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RearEndCollision
{
    class MapGeneratorFromFile : MapGenerator
    {
        public string Filename { get; private set; }

        public MapGeneratorFromFile(string filename)
        {
            Filename = filename;
        }
        public override char[,] GenerateMap()
        {
            int rows = 0;
            int cols = 0;

            using (StreamReader file = new StreamReader(Filename))
            {
                while (true)
                {
                    string line = file.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    rows++;
                    cols = Math.Max(cols, line.Length);
                }
            }

            char[,] map = new char[rows, cols];
            int currow = 0;
            using (StreamReader file = new StreamReader(Filename))
            {
                while (true)
                {
                    string line = file.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    for (int i = 0; i < line.Length; i++)
                    {
                        map[currow, i] = (line[i] == ' ' ? ' ' : '#');
                    }
                    currow++;
                    cols = Math.Max(cols, line.Length);
                }
            }

            return map;
        }
    }
}
