using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RearEndCollision
{
    class MapGenerateEmpty : MapGenerator
    {
        //There is no : abstract base.Class implementation
        // TODO: to be removed with functional implementation
        private int rows;
        private int columns;

        public MapGenerateEmpty(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
        }
        // use file versions - this is doing nothing
        public override char[,] GenerateMap()
        {
            char[,] generatedMap = new char[rows, columns];
            for (int i = 1; i < rows - 1; i++)
            {
                for (int j = 1; j < columns - 1; j++)
                {
                    generatedMap[i, j] = ' ';
                }
            }
            for (int i = 0; i < columns; i++)
            {
                generatedMap[0, i] = generatedMap[rows - 1, i] = '#';
            }
            for (int i = 0; i < rows; i++)
            {
                generatedMap[i, 0] = generatedMap[i, columns - 1] = '#';
            }
            return generatedMap;
        }
    }
}
