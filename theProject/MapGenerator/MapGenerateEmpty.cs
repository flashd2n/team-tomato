﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RearEndCollision
{
    public class MapGenerateEmpty : MapGenerator
    {
        
        private int rows;
        private int columns;

        public MapGenerateEmpty(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
        }
        
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
